using System.Net.Sockets;
using System.Text;

namespace Console_Shooter_Host;

public class Server
{
    private NetworkManager _networkManager;
    public ClientManager ClientManager = new();
    public LoginManager LoginManager = new();

    private Thread _clientAccepter;
    
    public Server(string ip, int port)
    {
        _networkManager = new NetworkManager(ip, port);

        _clientAccepter = new Thread(AcceptClients);
        _clientAccepter.Start();
    }

    public void AcceptClients()
    {
        try
        {
            while (true)
            {
                var tcpClient = _networkManager.AcceptClient();

                var newClient = new Client(tcpClient, new LoginRequestHandler());
                
                var clientGuid = ClientManager.Add(newClient);
                
                Thread receiverThread = new Thread(() => HandleClient(clientGuid));
                receiverThread.Start();
            }
        }
        catch (ThreadInterruptedException e)
        {
        }
    }

    public void HandleClient(Guid clientId)
    {
        while (true)
        {
            var client = ClientManager.GetClient(clientId);
            
            PacketType type;
            type = (PacketType)client.Receive(1)[0];

            UInt32 length;
            length = BitConverter.ToUInt32(client.Receive(sizeof(UInt32)));

            string json;
            json = Encoding.UTF8.GetString(client.Receive(length));

            Packet packet = new(type, json);

            if (client.RequestHandler.IsPacketRelevant(type))
            {
                client.RequestHandler.HandlePacket(this, packet, clientId);
            }
            else
            {
                // handle non-relevant packets
            }
        }
    }

}