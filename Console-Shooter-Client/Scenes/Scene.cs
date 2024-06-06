using Console_Shooter_Client.Objects;
using Console_Shooter_Client.Rendering;
using Guid = System.Guid;

namespace Console_Shooter_Client.Scenes;

public abstract class Scene
{
    private List<GameObject> _sceneObjects = new();

    public abstract void Start();

    public virtual void Update(float deltaTime)
    {
        foreach (var @object in _sceneObjects)
        {
            @object.Update(deltaTime);
        }
    }

    public abstract void Delete();

    public List<GameObject> GetSceneObjects()
    {
        return _sceneObjects;
    }

    public void RemoveObject(GameObject sceneObject)
    {
        foreach (var @object in _sceneObjects)
        {
            if (@object.Id == sceneObject.Id)
            {
                _sceneObjects.Remove(@object);
                return;
            }
        }
    }

    public T CreateObject<T>(Coords coords, int layer, params object?[]? args) where T : GameObject
    {
        var newObject = GameObject.CreateObject<T>(coords, layer, this, args);
        
        _sceneObjects.Add(newObject);
        newObject.Start();

        return newObject;
    }
}