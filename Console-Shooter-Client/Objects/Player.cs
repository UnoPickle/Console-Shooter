using Console_Shooter_CLient.Drivers;
using Console_Shooter_Client.Rendering;
using Console_Shooter_Client.Scenes;

namespace Console_Shooter_Client.Objects;

public class Player : GameObject
{
    public override void Start()
    {
    }

    public override void Update(float deltaTime)
    {
    }

    public override void Deleted()
    {
        
    }

    public override WindowsDriver.CharInfo[,] GetVisualData()
    {
        return new WindowsDriver.CharInfo[,]
        {
            {new(' ', ColorUtils.GetColorCode(Color.White, Color.White)), new(' ', ColorUtils.GetColorCode(Color.White, Color.White))},
            {new(' ', ColorUtils.GetColorCode(Color.White, Color.White)), new(' ', ColorUtils.GetColorCode(Color.White, Color.White))}
        };
    }
}