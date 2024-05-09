namespace Console_Shooter_Host;

public static class Program
{
    
    public static void Main(string[] args)
    {
        Server server = new Server(args[0], uint.Parse(args[1]), args[2]);
    }
}
