using System.Text;
using Console_Shooter_Host.Packets;
using Console_Shooter_Host.RequestHandler;
using Console_Shooter_Host.Utils;

namespace Console_Shooter_Host.Managers;

public class ClientManager(Server server)
{
    private List<Client> _clients = new();
    public void AddClient(Client client)
    {
        _clients.Add(client);

        Thread clientHandler = new Thread(() => HandleClient(client));
        clientHandler.Start();
    }

    public void UpdateRequestHandler(Client client, IRequestHandler requestHandler)
    {
        foreach (var clientEntry in  _clients)
        {
            if (clientEntry == client)
            {
                clientEntry.RequestHandler = requestHandler;
                return;
            }
        }
    }

    private void HandleClient(Client client)
    {
        while (client.IsConnected())
        {
            PacketType type = (PacketType)NetworkUtils.Read(client.TcpClient, 1)[0];
            
            uint length = BitConverter.ToUInt32(NetworkUtils.Read(client.TcpClient, 4));

            string json = Encoding.UTF8.GetString(NetworkUtils.Read(client.TcpClient, (int)length));

            Request request = new Request(type, json);
            
            if (client.RequestHandler.IsRequestRelevant(request))
            {
                client.RequestHandler.HandleRequest(client, request);
            }
        }
    }
}