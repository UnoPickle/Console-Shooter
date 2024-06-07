using Console_Shooter_CLient.Drivers;
using Console_Shooter_Client.Objects;
using Console_Shooter_Client.Scenes;

namespace Console_Shooter_Client.Rendering;

public class Renderer
{
    public const int ScreenHeight = 30, ScreenWidth = 120;
    
    private WindowsDriver _consoleDriver = new WindowsDriver();

    public void DrawScene(Scene scene)
    {
        var sceneObjects = scene.GetSceneObjects();

        var sortedObjects = Array.Empty<List<RenderObject>?>();

        foreach (var sceneObject in sceneObjects)
        {
            if ((sceneObject.Layer + 1) > sortedObjects.Length)
            {
                var prevObjectListSize = sortedObjects.Length;
                Array.Resize(ref sortedObjects, sceneObject.Layer + 1);

                for (int i = prevObjectListSize; i < sceneObject.Layer + 1; i++)
                {
                    sortedObjects[i] = new List<RenderObject>();
                }
            }

            sortedObjects[sceneObject.Layer].Add(new RenderObject(sceneObject, sceneObject.Coords));
        }

        WindowsDriver.CharInfo[,] frameBuffer = new WindowsDriver.CharInfo[ScreenHeight, ScreenWidth];

        for (int i = 0; i < sortedObjects.Length; i++)
        {
            foreach (var @object in sortedObjects[i])
            {
                var visualObjectData = @object.Object.GetVisualData();

                var rows = visualObjectData.GetLength(0);
                var columns = visualObjectData.GetLength(1);
                
                for (int col = 0; col < Math.Min(columns, frameBuffer.GetLength(1)); col++)
                {
                    for (int row = 0; row < Math.Min(rows, frameBuffer.GetLength(0)); row++)
                    {
                        frameBuffer[@object.Coords.Y + row, @object.Coords.X + col] = visualObjectData[row, col];
                    }
                }
            }
        }
        
        _consoleDriver.PrintBufferAt(frameBuffer, new Coords(0, 0));
    }
}