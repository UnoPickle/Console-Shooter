using Console_Shooter_Client.Drivers;
using Console_Shooter_Client.Renderer;

namespace Console_Shooter_Client.Visual_Objects;

public class Camera : VisualObject
{
    public Coords Coords = new Coords(0 ,0);

    private List<Renderer.Renderer.FrameObject>[] _currentFrameObjects =
        Array.Empty<List<Renderer.Renderer.FrameObject>>();

    public void RenderObject(VisualObject visualObject, Coords coords, int layer)
    {
        var currentFrameObjectsLength = _currentFrameObjects.Length;
        if (currentFrameObjectsLength < (layer + 1))
        {
            Array.Resize(ref _currentFrameObjects, (layer + 1));

            for (int i = currentFrameObjectsLength; i < _currentFrameObjects.Length; i++)
            {
                _currentFrameObjects[i] = new List<Renderer.Renderer.FrameObject>();
            }
        }

        _currentFrameObjects[layer].Add(new Renderer.Renderer.FrameObject(visualObject, coords));
    }

    public override CharInfo[,] GetVisualData()
    {
        return GetNextFrame();
    }

    private CharInfo[,] GetNextFrame()
    {
        CharInfo[,] data = new CharInfo[Screen.DefaultHeight, Screen.DefaultWidth];

        for (int i = 0; i < _currentFrameObjects.Length; i++)
        {
            foreach (var frameObject in _currentFrameObjects[i])
            {
                var slice = GetObjectSlice(frameObject);

                if (slice == null)
                {
                    break;
                }
                
                var objectRect = frameObject.VisualObject.GetSizeRect();

                var width = objectRect.Right - slice?.Left - slice?.Right;
                var height = objectRect.Bottom - slice?.Top - slice?.Bottom;

                var visualData = frameObject.VisualObject.GetVisualData();

                for (short col = 0; col < width; col++)
                {
                    for (short row = 0; row < height; row++)
                    {
                        
                        var horizontalStartingIndex = (short)slice?.Left!;

                        var verticalStartingIndex = (short)slice?.Top!;
                        
                        var index = ConvertCoordsToIndex(new Coords((short)(col + frameObject.Coords.X + horizontalStartingIndex), (short)(row + frameObject.Coords.Y + verticalStartingIndex)));
                        data[index.Y, index.X] =
                            visualData[verticalStartingIndex + row, horizontalStartingIndex + col];
                    }
                }
            }
        }

        foreach (var frameObjects in _currentFrameObjects)
        {
            frameObjects.Clear();
        }

        return data;
    }

    private Coords ConvertCoordsToIndex(Coords coords)
    {
        return new Coords((short)(coords.X - this.Coords.X), (short)(coords.Y - this.Coords.Y));
    }
    
    public SmallRect? GetObjectSlice(Renderer.Renderer.FrameObject frameObject)
    {
        var objectRect = frameObject.VisualObject.GetSizeRect();

        var leftDiff = frameObject.Coords.X - Coords.X;

        if (leftDiff < 0)
        {
            if (-leftDiff > objectRect.Right)
            {
                return null;
            }

            leftDiff = Math.Abs(leftDiff);
        }
        else
        {
            leftDiff = 0;
        }

        var rightDiff = Coords.X + Screen.DefaultWidth - (frameObject.Coords.X + objectRect.Right);

        if (rightDiff < 0)
        {
            if (-rightDiff > objectRect.Right)
            {
                return null;
            }

            rightDiff = Math.Abs(rightDiff);
        }
        else
        {
            rightDiff = 0;
        }

        var topDiff = frameObject.Coords.Y - Coords.Y;

        if (topDiff < 0)
        {
            if (-topDiff > objectRect.Bottom)
            {
                return null;
            }

            topDiff = Math.Abs(topDiff);
        }
        else
        {
            topDiff = 0;
        }

        var bottomDiff = Coords.Y + Screen.DefaultHeight - (frameObject.Coords.Y + objectRect.Bottom);

        if (bottomDiff < 0)
        {
            if (-bottomDiff > objectRect.Bottom)
            {
                return null;
            }

            bottomDiff = Math.Abs(bottomDiff);
        }
        else
        {
            bottomDiff = 0;
        }


        return new SmallRect((short)leftDiff, (short)topDiff,
            (short)rightDiff,
            (short)bottomDiff);
    }
}