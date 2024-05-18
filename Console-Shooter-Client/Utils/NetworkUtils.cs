using Console_Shooter_Client.Visual_Objects.Game_Objects;

namespace Console_Shooter_Client.Utils;

public static class NetworkUtils
{
    public static Map DeserializeMap(byte[] serializedMap, uint rows, uint columns)
    {
        MapEntry[,] map = new MapEntry[rows, columns];

        for (int row = 0; row < rows; row++)
        {
            for (int column = 0; column < columns; column ++)
            {
                var curSerializedMapIndex = (row * columns + column) * 3;

                MapEntry entry = new MapEntry((Color)serializedMap[curSerializedMapIndex],
                    serializedMap[curSerializedMapIndex + 1] == 1, serializedMap[curSerializedMapIndex + 2]);
                map[row, column] = entry;
            }
        }

        return new Map(map);
    }
}