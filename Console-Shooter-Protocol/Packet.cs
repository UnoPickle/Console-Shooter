namespace Console_Shooter_Protocol;

public struct Packet(PacketType type, string json)
{
    public PacketType Type = type;
    public string Json = json;
}

public enum PacketType : byte
{
    ErrorPacket = 0,
    LoginPacket = 1,
    GetMapPacket = 2,
    UpdatePositionPacket = 3
}