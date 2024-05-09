using Console_Shooter_Host.Exceptions;

namespace Console_Shooter_Host.Managers;

public class LoginManager
{
    private List<Player> _loggedPlayers = new List<Player>();

    public void AddPlayer(Player player)
    {
        Console.WriteLine($"[Login Manager]: New player! ({player.Username})");
        
        if (!_loggedPlayers.Contains(player))
        {
            _loggedPlayers.Add(player);
        }
        else
        {
            throw new LoginManagerException("user already exists");
        }
    }

    public void RemoveUser(Player player)
    {
        Console.WriteLine($"[Login Manager]: {player.Username} logged off");
        
        if (!_loggedPlayers.Contains(player))
        {
            _loggedPlayers.Remove(player);
        }
        else
        {
            throw new LoginManagerException("user doesn't exist");
        }
    }
}
