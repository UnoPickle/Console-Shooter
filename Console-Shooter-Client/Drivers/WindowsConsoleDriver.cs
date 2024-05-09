using System.Runtime.InteropServices;
using Microsoft.Win32.SafeHandles;

namespace Console_Shooter_Client.Drivers;

public class WindowsConsoleDriver
{
    private const int STD_OUTPTUT_HANDLE = -11;
    private const int STD_INPUT_HANDLE = -10;
    private const int STD_ERROR_HANDLE = -12;

    private IntPtr InputHandle, OutputHandle;

    private static ConsoleCursorInfo _originalCursorInfo;

    public WindowsConsoleDriver()
    {
        InputHandle = GetStdHandle(STD_INPUT_HANDLE);
        OutputHandle = GetStdHandle(STD_OUTPTUT_HANDLE);

        GetConsoleCursorInfo(OutputHandle, out _originalCursorInfo);
    }

    public void WriteToConsole(CharInfo[,] data, Coords dataCoords, SmallRect rect)
    {
        CharInfo[] dataArray = new CharInfo[data.Length];

        var rows = data.GetLength(0);
        var columns = data.GetLength(1);
        
        
        for (int row = 0; row < rows; row++)
        {
            for (int col = 0; col < columns; col++)
            {
                dataArray[row * columns + col] = data[row, col];
            }
        }

        WriteConsoleOutput(OutputHandle, dataArray, new Coords((short)columns, (short)rows),
            dataCoords, ref rect);
    }

    [DllImport("kernel32.dll", SetLastError = true)]
    private static extern IntPtr GetStdHandle(int nStdHandle);

    [DllImport("kernel32.dll", SetLastError = true)]
    private static extern bool GetConsoleCursorInfo(IntPtr hConsoleOutput, out ConsoleCursorInfo lpConsoleCursorInfo);

    [DllImport("kernel32.dll", SetLastError = true)]
    private static extern bool WriteConsoleOutput(IntPtr hConsoleOutput, CharInfo[] lpBuffer, Coords dwBufferSize,
        Coords dwBufferCoord, ref SmallRect lpWriteRegion);

    public const int MF_BYCOMMAND = 0x00000000;
    
    public const int SC_SIZE = 0xF000;
    public const int SC_MINIMIZE = 0xF020;
    public const int SC_MAXIMIZE = 0xF030;
    
    // TODO: replace with an option to gray out instead of deleting
    [DllImport("user32.dll", SetLastError = true)]
    public static extern int DeleteMenu(IntPtr hMenu, int nPosition, int wFlags);

    [DllImport("user32.dll", SetLastError = true)]
    public static extern IntPtr GetSystemMenu(IntPtr hWnd, bool bRevert);

    [DllImport("kernel32.dll", SetLastError = true)]
    public static extern IntPtr GetConsoleWindow();

    [DllImport("user32.dll", SetLastError = true)]
    public static extern short GetKeyState(int nVirtKey);
    
    [StructLayout(LayoutKind.Sequential)]
    public struct ConsoleCursorInfo
    {
        public uint DwSize;
        public bool bVisible;
    }

}

[StructLayout(LayoutKind.Sequential)]
public struct SmallRect(short left, short top, short right, short bottom)
{
    public short Left = left;
    public short Top = top;
    public short Right = right;
    public short Bottom = bottom;
}
    
public struct Coords(short x, short y)
{
    public short X = x;
    public short Y = y;
        
}
    

[StructLayout(LayoutKind.Explicit, CharSet = CharSet.Unicode)]
public struct CharInfo(char c, ushort attributes)
{
    [FieldOffset(0)] public char Char = c;
    [FieldOffset(2)] public ushort Attributes = attributes;
}