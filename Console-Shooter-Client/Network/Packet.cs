namespace Console_Shooter_Client.Network;

public class Packet(PacketType type, string json)
{
    public PacketType Type = type;
    public string Json = json;
}