using Console_Shooter_Client.Drivers;

namespace Console_Shooter_Client;

public class PrintDevice
{
    private WindowsConsoleDriver _consoleDriver = new();

    public void PrintAt(CharInfo[,] data, Coords coords)
    {
        _consoleDriver.WriteToConsole(data, new Coords(0, 0),
            new SmallRect(coords.X, coords.Y, (short)(coords.X + data.GetLength(1)),
                (short)(coords.Y + data.GetLength(0))));
    }
}