using System.Net;
using System.Net.Sockets;

namespace Console_Shooter_Host;

public class NetworkManager
{
    private TcpListener _listener;

    public NetworkManager(string ip, int port)
    {
        _listener = new TcpListener(IPAddress.Parse(ip), port);
        _listener.Start();
    }

    public TcpClient AcceptClient()
    {
        return _listener.AcceptTcpClient();
    }

}