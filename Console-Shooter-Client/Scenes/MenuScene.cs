using System.Diagnostics.Tracing;
using Console_Shooter_Client.Drivers;
using Console_Shooter_Client.Network.Packets;
using Console_Shooter_Client.Visual_Objects;

namespace Console_Shooter_Client.Scenes;

public class MenuScene(Game game) : Scene(game)
{
    private Title _enterUsername = new Title("Enter username: ");
    private bool _isOnUsername = true;
    private string _username = "";

    public override void Start()
    {
    }

    public override void Update(float elapsedTime)
    {
        if (Console.KeyAvailable)
        {
            var key = Console.ReadKey();

            if (key.Key == ConsoleKey.Enter)
            {
                game.ProtocolManager.Login(new LoginRequest(_username));
                game.SceneManager.SwitchScene<GameScene>();
            }
            else
            {
                _username += key.KeyChar;
                _enterUsername.Text = $"Enter username: {_username}";
            }
        }

        game.Renderer.RenderObject(_enterUsername, new Coords(0, 0), 0);
        base.Update(elapsedTime);
    }
}