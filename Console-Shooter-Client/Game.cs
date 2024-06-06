using Console_Shooter_Client.Scenes;

namespace Console_Shooter_Client;

public class Game
{
    private Thread _loopThread;
    private EventHandler<float>? _updateHook;

    private GameManager _gameManager;
    
    public Game()
    {
        _loopThread = new Thread(Loop);
        _loopThread.Start();

        _gameManager = new GameManager(new GameScene(), ref _updateHook);
    }

    private void Loop()
    {
        DateTime prevFrameTime = DateTime.Now;

        while (true)
        {
            var current = DateTime.Now;

            TimeSpan elapsedTime = current - prevFrameTime;
            prevFrameTime = DateTime.Now;
            
            _updateHook?.Invoke(null, (float)elapsedTime.TotalSeconds);
        }
    }
}