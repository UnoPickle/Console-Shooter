using Console_Shooter_Host.Packets;

namespace Console_Shooter_Host;

public struct Request(PacketType type, string json)
{
    public PacketType Type = type;
    public string Json = json;
}