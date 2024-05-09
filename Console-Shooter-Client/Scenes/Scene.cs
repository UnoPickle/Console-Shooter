using System.Diagnostics;

namespace Console_Shooter_Client.Scenes;

public abstract class Scene(Game game)
{
    protected ObjectHandler ObjectHandler = new();
    
    public abstract void Start();

    public virtual void Close()
    {
        ObjectHandler.Close();
    }

    public virtual void Update(float elapsedTime)
    {
        ObjectHandler.Update(elapsedTime);
    }
}