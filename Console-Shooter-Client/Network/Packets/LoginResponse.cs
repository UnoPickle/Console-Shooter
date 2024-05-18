namespace Console_Shooter_Client.Network.Packets;

public class LoginResponse(UInt32 status)
{
    public const UInt32 Successful = 1;
    public const UInt32 Unsuccessful = 0;
    
    public UInt32 status = status;
}