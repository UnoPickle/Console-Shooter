using System.Text;
using Console_Shooter_Client.Exceptions;
using Console_Shooter_Client.Network.Packets;

namespace Console_Shooter_Client.Network;

public class ProtocolManager(NetworkHandler networkHandler)
{
    
    private NetworkHandler _networkHandler = networkHandler;

    
    public static byte[] PreparePacket<T>(PacketType packetType, T packet)
    {
        var json = Newtonsoft.Json.JsonConvert.SerializeObject(packet);
        var packetSize = sizeof(PacketType) + sizeof(uint) + json.Length;

        byte[] packetBytes = new byte[packetSize];
        packetBytes[0] = (byte)packetType;

        Array.Copy(BitConverter.GetBytes(json.Length), 0, packetBytes, sizeof(PacketType), sizeof(uint));
        Array.Copy(Encoding.UTF8.GetBytes(json), 0, packetBytes, sizeof(PacketType) + sizeof(uint), json.Length);

        return packetBytes;
    }

    private Mutex _incomingPacketsMutex = new();
    private List<Packet> _incomingPackets = new List<Packet>();

    private Thread? _getPacketsThread;

    public void GetPackets()
    {
        if (_getPacketsThread != null)
        {
            _getPacketsThread.Interrupt();
        }

        _getPacketsThread = new(() =>
        {
            try
            {
                while (_networkHandler.IsConnected())
                {
                    PacketType type = (PacketType)_networkHandler.Receive(1)[0];

                    uint length = BitConverter.ToUInt32(_networkHandler.Receive(sizeof(uint)));

                    string json = Encoding.UTF8.GetString(_networkHandler.Receive((int)length));

                    _incomingPacketsMutex.WaitOne();
                    _incomingPackets.Add(new Packet(type, json));
                    _incomingPacketsMutex.ReleaseMutex();
                }
            }
            catch (ThreadInterruptedException e)
            {
            }
        });

        _getPacketsThread.Start();
    }

    private const int GetPacketMillisecondsDelay = 10;
    
    public Packet GetPacket(PacketType type)
    {
        while (true)
        {
            _incomingPacketsMutex.WaitOne();
            foreach (var packet in _incomingPackets)
            {
                if (packet.Type == PacketType.ErrorPacket)
                {
                    throw new ProtocolException("received an error packet!");
                }

                if (packet.Type == type)
                {
                    _incomingPacketsMutex.ReleaseMutex();
                    return packet;
                }
            }
            _incomingPacketsMutex.ReleaseMutex();

            Thread.Sleep(GetPacketMillisecondsDelay);
        }
    }

    public LoginResponse Login(LoginRequest request)
    {
        _networkHandler.Write(PreparePacket(PacketType.LoginPacket, request));

        Packet response = GetPacket(PacketType.LoginPacket);

        return Newtonsoft.Json.JsonConvert.DeserializeObject<LoginResponse>(response.Json);
    }

    public GetMapResponse GetMap(GetMapRequest request)
    {
        _networkHandler.Write(PreparePacket(PacketType.GetMapPacket, request));

        Packet response = GetPacket(PacketType.GetMapPacket);

        return Newtonsoft.Json.JsonConvert.DeserializeObject<GetMapResponse>(response.Json);
    }
}

public enum PacketType : byte
{
    ErrorPacket = 0,
    LoginPacket = 1,
    GetMapPacket = 2,
    UpdatePositionPacket = 3
}