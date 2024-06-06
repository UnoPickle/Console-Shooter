using Console_Shooter_Client.Rendering;

namespace Console_Shooter_Client.Objects;

public class Map : GameObject
{
    private MapEntry[,] _map;

    public Map(MapEntry[,] map)
    {
        _map = map;
    }

    public static Map? LoadMap(byte[] map, int rows, int columns)
    {
        if (map.Length % 3 != 0)
        {
            return null;
        }

        MapEntry[,] mapEntries = new MapEntry[rows, columns];
        
        for (int row = 0; row < rows; row++)
        {
            for (int col = 0; col < columns; col++)
            {
                var index = row * columns * 3 + col;

                mapEntries[row, col] = new MapEntry(
                    (Color)map[index], BitConverter.ToBoolean(map, index + 1), map[index + 2]);
            }
        }

        return new Map(mapEntries);
    }

    /*public bool CheckCollision(WindowsDriver.SmallRect colliderLocRect)
    {
        var mapLocRect = GetLocRect();
        
        if (colliderLocRect.Left >= mapLocRect.Right || colliderLocRect.Right <= mapLocRect.Left ||
            colliderLocRect.Top >= mapLocRect.Bottom || colliderLocRect.Bottom <= mapLocRect.Top)
        {
            return false;
        }

        for (int row = colliderLocRect.Top; row < colliderLocRect.Bottom; row++)
        {
            for (int col = colliderLocRect.Left; col < colliderLocRect.Right; col++)
            {
                if (row < mapLocRect.Bottom && row >= mapLocRect.Top && (col / 2) < mapLocRect.Right &&
                    (col / 2) >= mapLocRect.Left && _map[row - Coords.Y, (col / 2) - Coords.X].IsCollidable)
                {
                    return true;
                }
            }
        }

        return false;
    }*/

    public override void Start()
    {
        
    }

    public override void Update(float deltaTime)
    {
        
    }

    public override void Deleted()
    {
        
    }

    public override WindowsDriver.CharInfo[,] GetVisualData()
    {
        var rows = _map.GetLength(0);
        var columns = _map.GetLength(1);
        
        var dataWidth = columns * 2;
        var dataHeight = rows;
        WindowsDriver.CharInfo[,] data = new WindowsDriver.CharInfo[dataHeight, dataWidth];

        for (int row = 0; row < rows; row++)
        {
            for (int col = 0; col < columns; col++)
            {
                var mapEntry = _map[row, col];
                data[row, (col * 2)] = new WindowsDriver.CharInfo(' ', ColorUtils.GetColorCode(mapEntry.Color, mapEntry.Color));
                data[row, (col * 2) + 1] = new WindowsDriver.CharInfo(' ', ColorUtils.GetColorCode(mapEntry.Color, mapEntry.Color));
            }
        }

        return data;
    }
    
}

public struct MapEntry(Color color, bool isCollidable, byte renderLayer)
{
    public readonly Color Color = color;
    public readonly bool IsCollidable = isCollidable;
    public readonly byte RenderLayer = renderLayer;
}