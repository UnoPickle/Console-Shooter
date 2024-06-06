using System.Text;
using Console_Shooter_Protocol;

namespace Console_Shooter_Host.Packets;

public static class PacketUtils
{
    public static byte[] PreparePacket<T>(PacketType type, T packet)
    {
        long packetSize = 0;

        packetSize += sizeof(PacketType);
        packetSize += sizeof(UInt32);
        
        var json = Newtonsoft.Json.JsonConvert.SerializeObject(packet);

        packetSize += json.Length;

        byte[] finalPacket = new byte[packetSize];
        finalPacket[0] = (byte)type;
        
        BitConverter.GetBytes(json.Length).CopyTo(finalPacket, sizeof(PacketType));
        
        Encoding.UTF8.GetBytes(json).CopyTo(finalPacket, sizeof(PacketType) + sizeof(UInt32));

        return finalPacket;
    }
}