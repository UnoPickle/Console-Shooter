namespace Console_Shooter_Host;

public class LoginManager
{
    private List<LoggedUser> _loggedUsers = new();

    public void Add(LoggedUser user)
    {
        Console.WriteLine($"New login! {user.Username}");
        _loggedUsers.Add(user);
    }

    public void Remove(LoggedUser user)
    {
        _loggedUsers.Remove(user);
    }

    public void Remove(Guid clientId)
    {
        foreach (var loggedUser in _loggedUsers)
        {
            if (loggedUser.ClientId == clientId)
            {
                Remove(loggedUser);
                return;
            }
        }
    }
}