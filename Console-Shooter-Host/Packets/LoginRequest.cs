namespace Console_Shooter_Host.Packets;

public class LoginRequest
{
    public string username;

    public LoginRequest(string username)
    {
        this.username = username;
    }
}