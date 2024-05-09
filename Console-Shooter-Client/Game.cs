using System.Runtime.Serialization.Json;
using Console_Shooter_Client.Drivers;
using Console_Shooter_Client.Network;
using Console_Shooter_Client.Scenes;
using Console_Shooter_Client.Utils;
using Console_Shooter_Client.Visual_Objects;

namespace Console_Shooter_Client;

public class Game
{
    private const string ServerIp = "127.0.0.1";
    
    private FrameLoop _frameLoop;
    private PrintDevice _printDevice;
    private NetworkHandler _networkHandler;
    
    public Renderer.Renderer Renderer;
    public SceneManager SceneManager;
    public ProtocolManager ProtocolManager;
    
    public Game()
    {
        
        _printDevice = new();
        Renderer = new(_printDevice);
        /*_networkHandler = new(ServerIp);
        ProtocolManager = new(_networkHandler);
        ProtocolManager.GetPackets();*/
        
        
        _frameLoop = new();
        SceneManager = new(new GameScene(this), ref _frameLoop.UpdateHook, this);
        
        
        _frameLoop.Start();
        
        _frameLoop.UpdateHook += Update;
    }
    
    private void Update(object? o, float elapsedTime)
    {
        Renderer.RenderFrame();
    }
}