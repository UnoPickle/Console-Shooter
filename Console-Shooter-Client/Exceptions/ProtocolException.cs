namespace Console_Shooter_Client.Exceptions;

public class ProtocolException : ConsoleShooterException
{
    public ProtocolException(string message) : base("Protocol Exception: " + message)
    {
    }
}