using Console_Shooter_Client.Drivers;
using Console_Shooter_Client.Renderer;
using Console_Shooter_Client.Utils;

namespace Console_Shooter_Client.Visual_Objects;

public class Title(string text) : VisualObject
{
    public string Text = text;

    public override CharInfo[,] GetVisualData()
    {
        return StringUtils.ConvertString(Text, Color.White, Color.Black);
    }
}