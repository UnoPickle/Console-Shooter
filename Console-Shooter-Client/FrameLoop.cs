namespace Console_Shooter_Client;

public class FrameLoop
{
    public EventHandler<float>? UpdateHook;

    private Thread _loopThread;

    private bool _isAlive = true;

    public FrameLoop()
    {
        _loopThread = new Thread(Loop);
    }

    public void Start()
    {
        _loopThread.Start();
    }

    public void Stop()
    {
        _isAlive = false;
    }

    private void Loop()
    {
        DateTime prevFrameTime = DateTime.Now;

        while (_isAlive)
        {
            var current = DateTime.Now;

            TimeSpan elapsedTime = current - prevFrameTime;
            prevFrameTime = current;
            
            UpdateHook?.Invoke(null, (float)elapsedTime.TotalSeconds);
        }
    }
}