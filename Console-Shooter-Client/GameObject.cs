using Console_Shooter_Client.Drivers;
using Console_Shooter_Client.Renderer;

namespace Console_Shooter_Client;

public abstract class GameObject : VisualObject
{
    public abstract void Start();

    public abstract void Update(float elapsedTime);

    public abstract void Destroyed();

    public abstract override CharInfo[,] GetVisualData();

    public override SmallRect GetSizeRect()
    {
        return base.GetSizeRect();
    }
}