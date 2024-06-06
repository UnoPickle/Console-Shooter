namespace Console_Shooter_Client.Rendering;

public static class ColorUtils
{
    public static ushort GetColorCode(Color foreground, Color background)
    {
        return (ushort)(((ushort)foreground) | (((ushort)background) << 4));
    }
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