namespace Console_Shooter_Host.Packets;

public class ChangeSettingsResponse(uint status)
{
    public const uint Successful = 1;
    public const uint Unsuccessful = 0;
    
    public uint status = status;
}