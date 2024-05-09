using Console_Shooter_Host.Packets;

namespace Console_Shooter_Host.RequestHandler;

public class LoginRequestHandler(Server server) : IRequestHandler
{
    public bool IsRequestRelevant(Request request)
    {
        return request.Type == PacketType.LoginPacket;
    }

    public void HandleRequest(Client client, Request request)
    {
        switch (request.Type)
        {
            case PacketType.LoginPacket:
                try
                {
                    var player =
                        new Player(Newtonsoft.Json.JsonConvert.DeserializeObject<LoginRequest>(request.Json).username,
                            client);
                    server.LoginManager.AddPlayer(player);
                    Protocol.Respond(PacketType.LoginPacket, new LoginResponse(LoginResponse.Successful), client);
            
                    server.ClientManager.UpdateRequestHandler(client, new RoomRequestHandler(server, player));
                }
                catch (Exception e)
                {
                    Protocol.Respond(PacketType.LoginPacket, new LoginResponse(LoginResponse.Unsuccessful), client);
                }
                break;
            default:
                break;
        }
    }
}