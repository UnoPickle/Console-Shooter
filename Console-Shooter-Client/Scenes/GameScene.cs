using Console_Shooter_CLient.Drivers;
using Console_Shooter_Client.Objects;
using Console_Shooter_Client.Rendering;

namespace Console_Shooter_Client.Scenes;

public class GameScene : Scene
{
    public Camera Cam;
    public Map Map;
    public Player Player;

    public GameScene()
    {
        Cam = CreateObject<Camera>(new Coords(0, 0), 0, null);
        Map = Cam.CreateObject<Map>(new Coords(0, 0), 0, new MapEntry[,]
        {
            { new(Color.White, true, 0) },
            { new(Color.White, true, 0) }
        });

        Player = Cam.CreateObject<Player>(new Coords(10, 10), 0, null);
    }

    public override void Start()
    {

    }

    public override void Delete()
    {
        base.Delete();
    }

    private DateTime prevMoveTime = DateTime.MinValue;
    public void MovePlayer()
    {
        DateTime now = DateTime.Now;

        if (now > prevMoveTime.AddSeconds(0.02))
        {
            Coords tempCoords = Player.Coords;

            if (Input.GetKeyDown(Key.S))
            {
                tempCoords.Y += 1;
            }

            if (Input.GetKeyDown(Key.W))
            {
                tempCoords.Y -= 1;
            }

            if (Input.GetKeyDown(Key.A))
            {
                tempCoords.X -= 1;
            }

            if (Input.GetKeyDown(Key.D))
            {
                tempCoords.X += 1;
            }


            var sizeRect = Player.GetSizeRect();
            var tempMoveLocationRect = new WindowsDriver.SmallRect((short)(tempCoords.X + sizeRect.Left),
                (short)(tempCoords.Y + sizeRect.Top), (short)(tempCoords.X + sizeRect.Right),
                (short)(tempCoords.Y + sizeRect.Bottom));

            if (!Map.CheckCollision(tempMoveLocationRect))
            {
                Player.Coords = tempCoords;
            }

            prevMoveTime = now;
        }
    }

    public override void Update(float deltaTime)
    {
        base.Update(deltaTime);
        
        MovePlayer();

        Cam.ViewLocation.X = (short)(Player.Coords.X - Renderer.ScreenWidth / 2);
        Cam.ViewLocation.Y = (short)(Player.Coords.Y - Renderer.ScreenHeight / 2);
    }

}