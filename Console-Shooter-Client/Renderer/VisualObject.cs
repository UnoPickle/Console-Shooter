using Console_Shooter_Client.Drivers;

namespace Console_Shooter_Client.Renderer;

public abstract class VisualObject
{
    public Coords Coords;
    
    public abstract CharInfo[,] GetVisualData();

    public virtual SmallRect GetSizeRect()
    {
        var visualData = GetVisualData();
        return new SmallRect(0, 0, (short)visualData.GetLength(1), (short)visualData.GetLength(0));
    }

    public SmallRect GetLocRect()
    {
        var objectSizeRect = this.GetSizeRect();

        return new SmallRect((short)(objectSizeRect.Left + Coords.X), (short)(objectSizeRect.Top + Coords.Y),
           (short) (objectSizeRect.Right + Coords.X), (short)(objectSizeRect.Bottom + Coords.Y));
    }
}