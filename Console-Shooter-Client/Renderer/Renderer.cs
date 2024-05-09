using Console_Shooter_Client.Drivers;

namespace Console_Shooter_Client.Renderer;

public class Renderer(PrintDevice printDevice)
{
    private PrintDevice _printDevice = printDevice;

    private List<FrameObject>[] _currentFrameObjects = Array.Empty<List<FrameObject>>();

    public struct FrameObject(VisualObject visualObject, Coords coords)
    {
        public VisualObject VisualObject = visualObject;
        public Coords Coords = coords;
    }

    public void RenderObject(VisualObject visualObject, Coords coords, int layer)
    {
        var currentFrameObjectsLength = _currentFrameObjects.Length;
        if (currentFrameObjectsLength < (layer + 1))
        {
            Array.Resize(ref _currentFrameObjects, (layer + 1));

            for (int i = currentFrameObjectsLength; i < _currentFrameObjects.Length; i++)
            {
                _currentFrameObjects[i] = new List<FrameObject>();
            }
        }
        
        _currentFrameObjects[layer].Add(new FrameObject(visualObject, coords));
    }

    public void RenderFrame()
    {
        CharInfo[,] frameArray = new CharInfo[Screen.DefaultHeight, Screen.DefaultWidth];

        for (int i = 0; i < _currentFrameObjects.Length; i++)
        {
            foreach (var visualObject in _currentFrameObjects[i])
            {
                var visualObjectData = visualObject.VisualObject.GetVisualData();

                var rows = visualObjectData.GetLength(0);
                var columns = visualObjectData.GetLength(1);
                
                for (int col = 0; col < columns; col++)
                {
                    for (int row = 0; row < rows; row++)
                    {
                        frameArray[visualObject.Coords.Y + row, visualObject.Coords.X + col] = visualObjectData[row, col];
                    }
                }
            }
        }
        
        _printDevice.PrintAt(frameArray, new Coords(0, 0));

        foreach (var frameObjects in _currentFrameObjects)
        {
            frameObjects.Clear();
        }
    }
}