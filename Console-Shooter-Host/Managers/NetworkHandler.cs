using System.Net;
using System.Net.Sockets;
using Console_Shooter_Host.RequestHandler;

namespace Console_Shooter_Host.Managers;

public class NetworkHandler
{
    public bool IsActive;
    
    private TcpListener _tcpListener;
    private Server _server;
    public NetworkHandler(Server server, string ip, int port)
    {
        _server = server;
        _tcpListener = new TcpListener(IPAddress.Parse(ip), port);
        _tcpListener.Start();
        
        IsActive = true;
        
        Thread clientListener = new(() =>
        {
            while (IsActive)
            {
                AcceptConnection();
            }
        });

        clientListener.Start();
    }

    private void AcceptConnection()
    {
        TcpClient newClient = _tcpListener.AcceptTcpClient();
        Client client = new(newClient, new LoginRequestHandler(_server));
        _server.ClientManager.AddClient(client);
        
    }
}