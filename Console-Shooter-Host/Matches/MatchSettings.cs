namespace Console_Shooter_Host.Matches;

public struct MatchSettings(string matchName, uint numberOfPlayers)
{
    public string Name = matchName;
    public uint NumberOfPlayers = numberOfPlayers;
}