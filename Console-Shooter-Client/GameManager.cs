using Console_Shooter_Client.Rendering;
using Console_Shooter_Client.Scenes;

namespace Console_Shooter_Client;

public class GameManager
{
    private Mutex _sceneMutex = new Mutex();
    private Scene _scene;
    private Renderer _renderer = new();

    public GameManager(Scene scene, ref EventHandler<float>? handler)
    {
        _scene = scene;
        _scene.Start();
        
        handler += (object? o, float deltaTime) =>
        {
            Update(deltaTime);
        };
    }

    private void Update(float deltaTime)
    {
        _sceneMutex.WaitOne();
        
        _scene.Update(deltaTime);
        _renderer.DrawScene(_scene);
        
        _sceneMutex.ReleaseMutex();
    }

    public void SwitchScene<T>(params object?[]? args) where T : Scene
    {
        _sceneMutex.WaitOne();
        
        _scene.Delete();
        
        var newScene = (Scene)Activator.CreateInstance(typeof(T), args);

        _scene = newScene;
        newScene.Start();
        
        _sceneMutex.ReleaseMutex();
    }
}