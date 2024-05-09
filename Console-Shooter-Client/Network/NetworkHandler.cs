using System.Net.Sockets;

namespace Console_Shooter_Client.Network;

public class NetworkHandler
{
    private const int Port = 3535;
    
    private TcpClient _tcpClient;

    public NetworkHandler(string ip)
    {
        _tcpClient = new(ip, Port);
    }

    public bool IsConnected()
    {
        return _tcpClient.Connected;
    }

    public byte[] Receive(int len)
    {
        byte[] bytes = new byte[len];

        int bytesRead = 0;

        while (bytesRead < len)
        {
            if (_tcpClient.GetStream().DataAvailable)
            {
                byte[] tempBuffer = new byte[len];
                int readLen = _tcpClient.GetStream().Read(tempBuffer, 0, len);
                tempBuffer.CopyTo(bytes, bytesRead);

                bytesRead += readLen;
            }
        }

        return bytes;
    }

    public void Write(byte[] data)
    {
        _tcpClient.GetStream().Write(data);
    }
}