using System.Net.Sockets;

namespace Console_Shooter_Host.Utils;

public static class NetworkUtils
{
    public static byte[] Read(TcpClient client, int len)
    {
        byte[] bytes = new byte[len];

        int bytesRead = 0;

        while (bytesRead < len)
        {
            if (client.GetStream().DataAvailable)
            {
                byte[] tempBuffer = new byte[len];
                int readLen = client.GetStream().Read(tempBuffer, 0, len);
                tempBuffer.CopyTo(bytes, bytesRead);

                bytesRead += readLen;
            }
        }

        return bytes;
    }

    public static void Send(TcpClient client, byte[] data)
    {
        client.GetStream().Write(data);
    }
}