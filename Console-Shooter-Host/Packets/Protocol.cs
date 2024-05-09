using System.Text;
using Console_Shooter_Host.Utils;

namespace Console_Shooter_Host.Packets;

public static class Protocol
{
    private static byte[] PreparePacket<T>(PacketType packetType, T packet)
    {
        var json = Newtonsoft.Json.JsonConvert.SerializeObject(packet);
        var packetSize = sizeof(PacketType) + sizeof(uint) + json.Length;

        byte[] packetBytes = new byte[packetSize];
        packetBytes[0] = (byte)packetType;

        Array.Copy(BitConverter.GetBytes(json.Length), 0, packetBytes, sizeof(PacketType), sizeof(uint));
        Array.Copy(Encoding.UTF8.GetBytes(json), 0, packetBytes, sizeof(PacketType) + sizeof(uint), json.Length);

        return packetBytes;
    }

    public static void Respond<T>(PacketType type, T response, Client client)
    {
        NetworkUtils.Send(client.TcpClient, PreparePacket(type, response));
    }
}

public enum PacketType : byte
{
    LoginPacket = 1,
    StartGame = 2,
    ChangeSettings = 3,
    
}
