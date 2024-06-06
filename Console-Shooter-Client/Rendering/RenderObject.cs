using Console_Shooter_Client.Objects;

namespace Console_Shooter_Client.Rendering;

public struct RenderObject(GameObject o, Coords coords)
{
    public GameObject Object = o;
    public Coords Coords = coords;
}