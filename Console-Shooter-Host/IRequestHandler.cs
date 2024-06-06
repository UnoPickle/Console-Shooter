using Console_Shooter_Protocol;

namespace Console_Shooter_Host;

public interface IRequestHandler
{
    public bool IsPacketRelevant(PacketType type);
    public void HandlePacket(Server server, Packet packet, Guid clientId);
}