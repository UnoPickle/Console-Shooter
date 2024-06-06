using Console_Shooter_Host.Packets;
using Console_Shooter_Protocol;
using Console_Shooter_Protocol.Packets;

namespace Console_Shooter_Host;

public class MenuRequestHandler : IRequestHandler
{
    public bool IsPacketRelevant(PacketType type)
    {
        return type == PacketType.GetMapPacket;
    }

    public void HandlePacket(Server server, Packet packet, Guid clientId)
    {
        switch (packet.Type)
        {
            case PacketType.GetMapPacket:
                Map map = new Map(new MapEntry[,]
                {
                    {new(Color.White, true, (byte)1), new(Color.White, true, (byte)1)}
                });
                
                var client = server.ClientManager.GetClient(clientId);
                
                client.Send(PacketUtils.PreparePacket(PacketType.GetMapPacket,
                    new GetMapResponse(NetworkUtils.SerializeMap(map), (uint)map.map.GetLength(0),
                        (uint)map.map.GetLength(1), GetMapResponse.Successful)));
                break;
        }
    }
}