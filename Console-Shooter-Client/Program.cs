
namespace Console_Shooter_Client;

public static class Program
{
    public static int Main(string[] args)
    {
        Console.Title = "Console Shooter";
        Console.CursorVisible = false;
        
        Game game = new Game();
        return 0;
    }
}