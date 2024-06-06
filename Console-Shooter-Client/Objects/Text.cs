using Console_Shooter_Client.Rendering;

namespace Console_Shooter_Client.Objects;

public class Text : GameObject
{
    public string Content;
    public Color Foreground;
    public Color Background;

    public Text(string content, Color foreground, Color background)
    {
        Content = content;
        Foreground = foreground;
        Background = background;
    }

    public override void Start()
    {
           
    }

    public override void Update(float deltaTime)
    {
        
    }

    public override void Deleted()
    {
        
    }

    public override WindowsDriver.CharInfo[,] GetVisualData()
    {
        var textLines = Content.Split('\n');

        int longestRow = 0;

        foreach (var line in textLines)
        {
            longestRow = Math.Max(longestRow, line.Length);
        }
        
        var dataBuffer = new WindowsDriver.CharInfo[textLines.Length, longestRow];

        for (int row = 0; row < textLines.Length; row++)
        {
            for (int col = 0; col < longestRow; col++)
            {
                dataBuffer[row, col].Char = textLines[row][col];
                dataBuffer[row, col].Attributes = ColorUtils.GetColorCode(Foreground, Background);
            }
        }

        return dataBuffer;
    }
}