namespace Console_Shooter_Host.Exceptions;

public class LoginManagerException(string message) : ConsoleShooterException("Login Manager Exception: " + message)
{
    
}