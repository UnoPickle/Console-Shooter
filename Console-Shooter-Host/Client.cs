using System.Net.Sockets;
using Console_Shooter_Host.RequestHandler;

namespace Console_Shooter_Host;

public class Client(TcpClient client, IRequestHandler requestHandler)
{
    public TcpClient TcpClient = client;
    public IRequestHandler RequestHandler = requestHandler;

    public bool IsConnected()
    {
        return TcpClient.Connected;
    }
}