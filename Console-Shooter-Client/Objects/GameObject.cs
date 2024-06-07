using Console_Shooter_CLient.Drivers;
using Console_Shooter_Client.Rendering;
using Console_Shooter_Client.Scenes;

namespace Console_Shooter_Client.Objects;

public abstract class GameObject
{
    public Coords Coords;
    public Scene Scene { get; private set; }
    public Guid Id { get; private set; }
    public int Layer { get; private set; }

    public abstract void Start();

    public abstract void Update(float deltaTime);

    public abstract void Deleted();

    public abstract WindowsDriver.CharInfo[,] GetVisualData();

    public WindowsDriver.SmallRect GetSizeRect()
    {
        var visualData = GetVisualData();
        return new WindowsDriver.SmallRect(0, 0, (short)visualData.GetLength(1), (short)visualData.GetLength(0));
    }

    public static T CreateObject<T>(Coords coords, int layer, Scene scene, params object?[]? args) where T : GameObject
    {
        var gameObject = (T)Activator.CreateInstance(typeof(T), args);
        gameObject.Coords = coords;
        gameObject.Scene = scene;
        gameObject.Layer = layer;
        gameObject.Id = new Guid();

        return gameObject;
    }

    public WindowsDriver.SmallRect GetLocationRect()
    {
        var sizeRect = GetSizeRect();

        return new WindowsDriver.SmallRect((short)(Coords.X + sizeRect.Left), (short)(Coords.Y + sizeRect.Top),
            (short)(Coords.X + sizeRect.Right), (short)(Coords.Y + sizeRect.Bottom));
    }
}