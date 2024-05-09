using Console_Shooter_Host.Matches;
using Console_Shooter_Host.Packets;

namespace Console_Shooter_Host.RequestHandler;

public class RoomRequestHandler(Server server, Player player) : IRequestHandler
{
    public bool IsRequestRelevant(Request request)
    {
        return request.Type is PacketType.StartGame or PacketType.ChangeSettings;
    }

    public void HandleRequest(Client client, Request request)
    {
        switch (request.Type)
        {
            case PacketType.StartGame:
                
                break;
            case PacketType.ChangeSettings:
                if (server.Match.admin != player.Username)
                {
                    Protocol.Respond(PacketType.ChangeSettings,
                        new ChangeSettingsResponse(ChangeSettingsResponse.Unsuccessful), client);
                }

                try
                {
                    var changeSettingsRequest =
                        Newtonsoft.Json.JsonConvert.DeserializeObject<ChangeSettingsRequest>(request.Json);
                    
                    server.Match.ChangeSettings(new MatchSettings(changeSettingsRequest?.room_name, (uint)changeSettingsRequest?.num_of_players));
                    Protocol.Respond(PacketType.ChangeSettings,
                        new ChangeSettingsResponse(ChangeSettingsResponse.Successful), client);
                }
                catch (Exception e)
                {
                    Protocol.Respond(PacketType.ChangeSettings,
                        new ChangeSettingsResponse(ChangeSettingsResponse.Unsuccessful), client);
                }

                break;
            default:
                throw new ArgumentOutOfRangeException();
        }
    }

    private void CreateRoom()
    {
        
    }

    private void JoinRoom()
    {
        
    }
}