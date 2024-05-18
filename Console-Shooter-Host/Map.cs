namespace Console_Shooter_Host;

public class Map(MapEntry[,] map)
{
    public MapEntry[,] map = map;
}

public enum Color : ushort
{
    Black = 0x0,
    Blue = 0x1,
    Green = 0x2,
    Aqua = 0x3,
    Red = 0x4,
    Purple = 0x5,
    Yellow = 0x6,
    White = 0x7,
    Gray = 0x8,
    LightBlue = 0x9,
    LightGreen = 0xA,
    LightAqua = 0xB,
    LightRed = 0xC,
    LightPurple = 0xD,
    LightYellow = 0xE,
    BrightWhite = 0xF
}

public struct MapEntry(Color color, bool isCollidable, byte renderLayer)
{
    public readonly Color Color = color;
    public readonly bool IsCollidable = isCollidable;
    public readonly byte RenderLayer = renderLayer;
}