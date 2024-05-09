namespace Console_Shooter_Host.Matches;

public enum MatchState
{
    InLobby,
    Playing
}

public class Match(MatchSettings settings, string adminUsername)
{
    public MatchState state { get; private set; } = MatchState.InLobby;
    public string admin { get; private set; } = adminUsername;

    public MatchSettings settings { get; private set; } = settings;

    private readonly List<Player> _players = new();
    
    public void ChangeSettings(MatchSettings newSettings)
    {
        settings = newSettings;
    }

    public bool AddPlayer(Player player)
    {
        if (_players.Count < settings.NumberOfPlayers)
        {
            _players.Add(player);
            return true;
        }

        return false;
    }

    public void RemovePlayer(Player player)
    {
        _players.Remove(player);
    }

    public List<Player> GetPlayers()
    {
        return _players;
    }
}