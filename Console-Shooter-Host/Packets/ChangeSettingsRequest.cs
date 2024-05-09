namespace Console_Shooter_Host.Packets;

public class ChangeSettingsRequest(string roomName, uint numOfPlayers)
{
    public string room_name = roomName;
    public uint num_of_players = numOfPlayers;

}