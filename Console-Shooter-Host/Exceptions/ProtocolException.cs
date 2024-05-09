namespace Console_Shooter_Host.Exceptions;

public class ProtocolException(string message) : ConsoleShooterException("Protocol Exception: " + message);