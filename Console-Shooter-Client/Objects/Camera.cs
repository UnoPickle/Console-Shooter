using Console_Shooter_CLient.Drivers;
using Console_Shooter_Client.Rendering;

namespace Console_Shooter_Client.Objects;

public class Camera : GameObject
{
    public Coords ViewLocation = new Coords(0 ,0);

    private List<GameObject>[] _currentFrameObjects =
        Array.Empty<List<GameObject>>();

    public T CreateObject<T>(Coords coords, int layer, params object?[]? args) where T : GameObject
    {
        var gameObject = GameObject.CreateObject<T>(coords, layer, Scene, args);
        
        var currentFrameObjectsLength = _currentFrameObjects.Length;
        if (currentFrameObjectsLength < (gameObject.Layer + 1))
        {
            Array.Resize(ref _currentFrameObjects, (gameObject.Layer + 1));

            for (int i = currentFrameObjectsLength; i < _currentFrameObjects.Length; i++)
            {
                _currentFrameObjects[i] = new List<GameObject>();
            }
        }

        _currentFrameObjects[gameObject.Layer].Add(gameObject);

        return gameObject;
    }

    public override void Start()
    {
        
    }

    public override void Update(float deltaTime)
    {
        foreach (var objects in _currentFrameObjects)
        {
            foreach (var @object in objects)
            {
                @object.Update(deltaTime);
            }
        }
    }

    public override void Deleted()
    {
        foreach (var objects in _currentFrameObjects)
        {
            foreach (var @object in objects)
            {
                @object.Deleted();
            }
        }
    }

    public override WindowsDriver.CharInfo[,] GetVisualData()
    {
        return GetNextFrame();
    }

    private WindowsDriver.CharInfo[,] GetNextFrame()
    {
        WindowsDriver.CharInfo[,] data = new WindowsDriver.CharInfo[Renderer.ScreenHeight, Renderer.ScreenWidth];

        for (int i = 0; i < _currentFrameObjects.Length; i++)
        {
            foreach (var gameObject in _currentFrameObjects[i])
            {
                var slice = GetObjectSlice(gameObject);

                if (slice == null)
                {
                    continue;
                }
                
                var objectRect = gameObject.GetSizeRect();

                var width = objectRect.Right - slice?.Left - slice?.Right;
                var height = objectRect.Bottom - slice?.Top - slice?.Bottom;

                var visualData = gameObject.GetVisualData();

                for (short col = 0; col < width; col++)
                {
                    for (short row = 0; row < height; row++)
                    {
                        
                        var horizontalStartingIndex = (short)slice?.Left!;

                        var verticalStartingIndex = (short)slice?.Top!;
                        
                        var index = ConvertCoordsToIndex(new Coords((short)(col + gameObject.Coords.X + horizontalStartingIndex), (short)(row + gameObject.Coords.Y + verticalStartingIndex)));
                        data[index.Y, index.X] =
                            visualData[verticalStartingIndex + row, horizontalStartingIndex + col];
                    }
                }
            }
        }

        return data;
    }

    private Coords ConvertCoordsToIndex(Coords coords)
    {
        return new Coords((short)(coords.X - this.ViewLocation.X), (short)(coords.Y - this.ViewLocation.Y));
    }
    
    public WindowsDriver.SmallRect? GetObjectSlice(GameObject frameObject)
    {
        var objectRect = frameObject.GetSizeRect();

        var leftDiff = frameObject.Coords.X - ViewLocation.X;

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

        var rightDiff = ViewLocation.X + Renderer.ScreenWidth - (frameObject.Coords.X + objectRect.Right);

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

        var topDiff = frameObject.Coords.Y - ViewLocation.Y;

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

        var bottomDiff = ViewLocation.Y + Renderer.ScreenHeight - (frameObject.Coords.Y + objectRect.Bottom);

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


        return new WindowsDriver.SmallRect((short)leftDiff, (short)topDiff,
            (short)rightDiff,
            (short)bottomDiff);
    }
}