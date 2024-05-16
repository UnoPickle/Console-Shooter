namespace Console_Shooter_Host.Packets;

public class LoginResponse(UInt32 status)
{
    public const UInt32 Unsuccessful = 0;
    public const UInt32 Successful = 1;
    
    public UInt32 status = status;
}