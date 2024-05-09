namespace Console_Shooter_Client;

public class ObjectHandler
{
    private List<GameObject> _gameObjects = new List<GameObject>();
    
    public void Update(float elapsedTime)
    {
        foreach (var gameObjectEntry in _gameObjects)
        {
            gameObjectEntry.Update(elapsedTime);
        }   
    }

    public void Destroy(GameObject gameObject)
    {
        gameObject.Destroyed();
        _gameObjects.Remove(gameObject);
    }

    public void Close()
    {
        foreach (var gameObjectEntry in _gameObjects)
        {
            Destroy(gameObjectEntry);
        }
    }

    public T Create<T>(params object?[]? args) where T : GameObject
    {
        var newObject = (T)Activator.CreateInstance(typeof(T), args);

        if (newObject == null)
        {
            throw new Exception("Couldn't create a new object!");
        }

        newObject.Start();
        _gameObjects.Add(newObject);
        return newObject;
    }
}