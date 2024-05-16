namespace Console_Shooter_Host;

public class LoggedUser(string username, Guid clientId)
{
    public readonly string Username = username;
    public readonly Guid ClientId = clientId;
}