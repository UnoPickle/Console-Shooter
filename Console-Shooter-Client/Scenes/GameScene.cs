using Console_Shooter_Client.Drivers;
using Console_Shooter_Client.Objects;
using Console_Shooter_Client.Renderer;
using Console_Shooter_Client.Utils;
using Console_Shooter_Client.Visual_Objects;
using Console_Shooter_Client.Visual_Objects.Game_Objects;

namespace Console_Shooter_Client.Scenes;

public class GameScene(Game game) : Scene(game)
{
    private Camera _cam = new();

    public Map Map = Visual_Objects.Game_Objects.Map.LoadMap(new byte[]
    {
        (byte)Color.White, BitConverter.GetBytes(true)[0], 1
    }, 1, 1);
    private Player _player;

    public override void Start()
    { 
        _player = ObjectHandler.Create<Player>(this);
        _player.Coords = new Coords(10, 10);

    }
    public override void Update(float elapsedTime)
    {
        _cam.Coords.X = (short)(_player.Coords.X - (Screen.DefaultWidth / 2));
        _cam.Coords.Y = (short)(_player.Coords.Y - (Screen.DefaultHeight / 2));
        
        _cam.RenderObject(Map, new Coords(0, 0), 1);
        _cam.RenderObject(_player, _player.Coords, 2);
        game.Renderer.RenderObject(_cam, new Coords(0, 0), 0);
        
        base.Update(elapsedTime);
    }
}