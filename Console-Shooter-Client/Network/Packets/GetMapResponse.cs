namespace Console_Shooter_Client.Network.Packets;

public class GetMapResponse(byte[] serializedMap, UInt32 status, UInt32 rows, UInt32 columns)
{
    public const UInt32 Successful = 1;
    public const UInt32 Unsuccessful = 0;
    
    public UInt32 status = status;
    public byte[] serialized_map = serializedMap;
    public UInt32 rows = rows;
    public UInt32 columns = columns;
}