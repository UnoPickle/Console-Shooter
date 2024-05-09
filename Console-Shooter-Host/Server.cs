using System.Text.RegularExpressions;
using System.Threading.Channels;
using Console_Shooter_Host.Managers;
using Console_Shooter_Host.Matches;
using Match = Console_Shooter_Host.Matches.Match;

namespace Console_Shooter_Host;

public class Server
{
    private const int Port = 3535;
    
    public NetworkHandler NetworkHandler;
    public ClientManager ClientManager;
    public LoginManager LoginManager;
    public Match Match;

    public Server(string matchName, uint numberOfPlayers, string admin)
    {
        ClientManager = new ClientManager(this);
        NetworkHandler = new NetworkHandler(this,"0.0.0.0", Port);
        LoginManager = new();
        Match = new(new MatchSettings(matchName, numberOfPlayers),admin);
    }
}