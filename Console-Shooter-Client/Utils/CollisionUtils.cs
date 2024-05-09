using Console_Shooter_Client.Drivers;
using Console_Shooter_Client.Renderer;
using Console_Shooter_Client.Visual_Objects.Game_Objects;

namespace Console_Shooter_Client.Utils;

public static class CollisionUtils
{
    public static bool CheckCollision(VisualObject collider, Coords colliderCoords, Map objectMap)
    {
        var colliderSizeRect = collider.GetSizeRect();
        var collisionObject = objectMap.CheckCollision(new SmallRect((short)(colliderSizeRect.Left + colliderCoords.X),
            (short)(colliderSizeRect.Top + colliderCoords.Y), (short)(colliderSizeRect.Right + colliderCoords.X),
            (short)(colliderSizeRect.Bottom + colliderCoords.Y)));

        return collisionObject;
    }
}