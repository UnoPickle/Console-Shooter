namespace Console_Shooter_Host;

public static class NetworkUtils
{
    public static byte[] SerializeMap(Map map)
    {
        var mapData = map.map;
        var mapDataRows = mapData.GetLength(0);
        var mapDataColumns = mapData.GetLength(1);
        
        byte[] serializedMap = new byte[mapDataColumns * mapDataRows * 3];
        
        for (int row = 0; row < mapDataRows; row++)
        {
            for (int column = 0; column < mapDataColumns; column++)
            {
                var startingIndexOffset = (row * mapDataColumns + column) * 3;
                var curEntry = mapData[row, column];
                serializedMap[startingIndexOffset] = (byte)curEntry.Color;
                serializedMap[startingIndexOffset + 1] = (byte)(curEntry.IsCollidable ? 1 : 0);
                serializedMap[startingIndexOffset + 2] = curEntry.RenderLayer;
            }
        }

        return serializedMap;
    }
}