
namespace Console_Shooter_Client.Scenes;

public class SceneManager
{
    private Scene _activeScene;
    private Game _game;

    public SceneManager(Scene firstScene, ref EventHandler<float>? updateHandler, Game game)
    {
        _activeScene = firstScene;
        _game = game;
        
        _activeScene.Start();
        
        updateHandler += Update;
    }

    private void Update(object? o, float elapsedTime)
    {
        _activeScene.Update(elapsedTime);
    }

    public void SwitchScene<T>()  where T : Scene
    {
        _activeScene.Close();
        var newScene = (T)Activator.CreateInstance(typeof(T), _game);
        if (newScene != null)
        {
            _activeScene = newScene;
            _activeScene.Start();
        }
        else
        {
            // TODO: add custom exceptions
            throw new Exception("couldn't create an instance of a new scene!");
        }
    }
}