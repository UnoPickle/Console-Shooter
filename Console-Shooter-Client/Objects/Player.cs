using System.Diagnostics;
using Console_Shooter_Client.Drivers;
using Console_Shooter_Client.Scenes;
using Console_Shooter_Client.Utils;

namespace Console_Shooter_Client.Objects;

public class Player(GameScene gameScene) : GameObject
{
    private GameScene _gameScene = gameScene;

    private const InputManager.Key Up = InputManager.Key.CharW;
    private const InputManager.Key Down = InputManager.Key.CharS;
    private const InputManager.Key Right = InputManager.Key.CharD;
    private const InputManager.Key Left = InputManager.Key.CharA;
    
    private readonly int _inputDelay = 20;

    // TODO: repalce with asset loading
    private CharInfo[,] _visual;


    public override void Destroyed()
    {
        throw new NotImplementedException();
    }
    
    public override CharInfo[,] GetVisualData()
    {
        return _visual;
    }

    public override void Start()
    {
        _visual = new CharInfo[,]
        {
            {
                new(' ', ColorUtils.GetColorCode(Color.White, Color.White)),
                new(' ', ColorUtils.GetColorCode(Color.White, Color.White))
            },
            {
                new(' ', ColorUtils.GetColorCode(Color.White, Color.White)),
                new(' ', ColorUtils.GetColorCode(Color.White, Color.White))
            }
        };
    }

    private DateTime _nextInput = DateTime.Now;

    public override void Update(float elapsedTime)
    {
        var now = DateTime.Now;

        if (_nextInput <= now)
        {
            var nextCoords = Coords;
            //TODO: replace with actual movement system
            if (InputManager.GetKeyDown(Up))
            {
                 nextCoords.Y--;
            }

            if (InputManager.GetKeyDown(Down))
            {
                nextCoords.Y++;
            }

            if (InputManager.GetKeyDown(Right))
            {
                nextCoords.X++;
            }

            if (InputManager.GetKeyDown(Left))
            {
                nextCoords.X--;
            }
            
            if (!CollisionUtils.CheckCollision(this, nextCoords, _gameScene.Map))
            {
                Coords = nextCoords;
            }
            
            _nextInput = now + TimeSpan.FromMilliseconds(_inputDelay);
        }
    }
}