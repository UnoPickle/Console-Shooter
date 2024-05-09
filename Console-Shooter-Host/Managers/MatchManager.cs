/*using Console_Shooter_Host.Matches;

namespace Console_Shooter_Host.Managers;

public class MatchManager
{
    private List<Match> _matches = new();
    private Random _random = new();

    public MatchManager(Server server)
    {
        
    }

    public Match CreateMatch(string name, uint numOfPlayers, Player admin)
    {
        uint id;

        do
        {
            id = (uint)_random.Next();
        } while (FindMatch(id) != null);

        Match newMatch = new(id, name, numOfPlayers);
        _matches.Add(newMatch);
        
        return newMatch;
    }

    public bool AddPlayerToMatch(uint id, Player player)
    {
        var match = FindMatch(id);

        if (match == null)
        {
            return false;
        }

        return match.AddPlayer(player);
    }

    public void RemovePlayer(Player player)
    {
        foreach (var match in _matches)
        {
            if (match.GetPlayers().Contains(player))
            {
                match.RemovePlayer(player);
            }
        }
    }

    private Match? FindMatch(uint id)
    {
        foreach (var match in _matches)
        {
            if (match.Id == id)
            {
                return match;
            }
        }

        return null;
    }
}*/