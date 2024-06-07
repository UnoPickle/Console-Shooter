using System.Runtime.InteropServices;
using Console_Shooter_Client.Rendering;

namespace Console_Shooter_CLient.Drivers;

public class WindowsDriver
{
    private const int STD_OUTPTUT_HANDLE = -11;
    private const int STD_INPUT_HANDLE = -10;
    private const int STD_ERROR_HANDLE = -12;

    private IntPtr InputHandle, OutputHandle;

    public WindowsDriver()
    {
        InputHandle = GetStdHandle(STD_INPUT_HANDLE);
        OutputHandle = GetStdHandle(STD_OUTPTUT_HANDLE);
    }


    public void PrintBufferAt(CharInfo[,] buffer, Coords coords)
    {
        CharInfo[] charInfoArray = new CharInfo[buffer.Length];

        var rows = buffer.GetLength(0);
        var columns = buffer.GetLength(1);

        for (int row = 0; row < rows; row++)
        {
            for (int col = 0; col < columns; col++)
            {
                charInfoArray[row * columns + col] = buffer[row, col];
            }
        }

        SmallRect sizeRect = new SmallRect(coords.X, coords.Y, (short)(coords.X + columns), (short)(coords.Y + rows));

        WriteConsoleOutput(OutputHandle, charInfoArray, new Coords((short)columns, (short)rows), coords, ref sizeRect);
    }

    public static bool IsKeyPressed(int key)
    {
        return (GetKeyState(key) & 0x8000) != 0;
    }

    [DllImport("kernel32.dll", SetLastError = true)]
    private static extern IntPtr GetStdHandle(int nStdHandle);

    [DllImport("kernel32.dll", SetLastError = true)]
    private static extern bool WriteConsoleOutput(IntPtr hConsoleOutput, CharInfo[] lpBuffer, Coords dwBufferSize,
        Coords dwBufferCoord, ref SmallRect lpWriteRegion);

    [DllImport("user32.dll", SetLastError = true)]
    private static extern short GetKeyState(int nVirtKey);

    [StructLayout(LayoutKind.Explicit, CharSet = CharSet.Unicode)]
    public struct CharInfo(char c, ushort attributes)
    {
        [FieldOffset(0)] public char Char = c;
        [FieldOffset(2)] public ushort Attributes = attributes;
    }

    [StructLayout(LayoutKind.Sequential)]
    public struct SmallRect(short left, short top, short right, short bottom)
    {
        public short Left = left;
        public short Top = top;
        public short Right = right;
        public short Bottom = bottom;
    }
}