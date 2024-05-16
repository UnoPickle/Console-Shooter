using Console_Shooter_Host.Packets;

namespace Console_Shooter_Host;

public class LoginRequestHandler : IRequestHandler
{
    public bool IsPacketRelevant(PacketType type)
    {
        return type == PacketType.LoginPacket;
    }

    public void HandlePacket(Server server, Packet packet, Guid clientId)
    {
        switch (packet.Type)
        {
            case PacketType.LoginPacket:
                var request = Newtonsoft.Json.JsonConvert.DeserializeObject<LoginRequest>(packet.Json);

                Login(server, request, clientId);
                server.ClientManager.GetClient(clientId).Send(PacketUtils.PreparePacket(PacketType.LoginPacket,
                    new LoginResponse(LoginResponse.Successful)));
                break;
            default:
                throw new ArgumentOutOfRangeException();
        }
    }

    private void Login(Server server, LoginRequest request, Guid clientId)
    {
        var newUser = new LoggedUser(request.username, clientId);
        server.LoginManager.Add(newUser);
    }


}