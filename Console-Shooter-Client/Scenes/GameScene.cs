using Console_Shooter_Client.Objects;
using Console_Shooter_Client.Rendering;

namespace Console_Shooter_Client.Scenes;

public class GameScene : Scene
{
    public GameScene()
    {
        var cam = CreateObject<Camera>(new Coords(0,0), 0, null);
        cam.CreateObject<Map>(new Coords(0,0 ), 0, new MapEntry[,]
        {
            {new(Color.White, true, 0)},
            {new(Color.White, true, 0)}
        });
    }

    public override void Start()
    {
        
    }

    public override void Delete()
    {
        
    }

    public override void Update(float deltaTime)
    {
        
        
        base.Update(deltaTime);
    }

}