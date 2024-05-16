using System.Net.Sockets;

namespace Console_Shooter_Host;

public struct Client(TcpClient tcpClient, IRequestHandler handler)
{
    public readonly TcpClient TcpClient = tcpClient;
    public IRequestHandler RequestHandler = handler;

    public byte[] Receive(uint len)
    {
        byte[] bytes = new byte[len];

        uint bytesRead = 0;

        while (bytesRead < len)
        {
            if (TcpClient.GetStream().DataAvailable)
            {
                byte[] tempBuffer = new byte[len];
                uint readLen = (uint)TcpClient.GetStream().Read(tempBuffer, 0, (int)len);
                tempBuffer.CopyTo(bytes, bytesRead);

                bytesRead += readLen;
            }
        }

        return bytes;
    }

    public void Send(byte[] data)
    {
        TcpClient.GetStream().Write(data, 0, data.Length);
    }
}