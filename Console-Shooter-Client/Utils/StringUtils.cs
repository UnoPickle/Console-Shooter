using Console_Shooter_Client.Drivers;

namespace Console_Shooter_Client.Utils;

public static class StringUtils
{
    /* Converts a string into a two-dimensional array of CharInfo.
     * \n starts a new row
     */
    public static CharInfo[,] ConvertString(string data, Color foreground, Color background)
    {
        string[] rows = data.Split('\n');

        int longestRowLength = 0;
        foreach (string row in rows)
        {
            if (longestRowLength < row.Length)
            {
                longestRowLength = row.Length;
            }
        }

        CharInfo[,] arrayData = new CharInfo[rows.Length, longestRowLength];

        ushort attributes = ColorUtils.GetColorCode(foreground, background);
        uint curRow = 0, curCol = 0;

        foreach (string row in rows)
        {
            foreach (char c in row)
            {
                arrayData[curRow, curCol].Char = c;
                arrayData[curRow, curCol].Attributes = attributes;
                curCol++;
            }

            curRow++;
            curCol = 0;
        }

        return arrayData;
    }
}