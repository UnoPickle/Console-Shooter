
using Console_Shooter_Client.Drivers;

namespace Console_Shooter_Client;

public static class Screen
{
    public const int DefaultHeight = 30;
    public const int DefaultWidth = 120;

    public static int screenWidth { get; private set; } = DefaultWidth;
    public static int screenHeight { get; private set; } = DefaultHeight;

    public static void SetScreen(int width, int height, bool resizable)
    {
        Console.SetWindowSize(width, height);

        screenWidth = width;
        screenHeight = height;

        if (!resizable)
        {
            // From FeniXb3 DisableConsoleResize.cs           
            
            WindowsConsoleDriver.DeleteMenu(
                WindowsConsoleDriver.GetSystemMenu(WindowsConsoleDriver.GetConsoleWindow(), false),
                WindowsConsoleDriver.SC_SIZE, WindowsConsoleDriver.MF_BYCOMMAND);
            WindowsConsoleDriver.DeleteMenu(
                WindowsConsoleDriver.GetSystemMenu(WindowsConsoleDriver.GetConsoleWindow(), false),
                WindowsConsoleDriver.SC_MAXIMIZE, WindowsConsoleDriver.MF_BYCOMMAND);
            WindowsConsoleDriver.DeleteMenu(
                WindowsConsoleDriver.GetSystemMenu(WindowsConsoleDriver.GetConsoleWindow(), false),
                WindowsConsoleDriver.SC_MINIMIZE, WindowsConsoleDriver.MF_BYCOMMAND);
        }
    }
}