using UnityEngine;

public static class GridHelper
{
    public static int GetColumn(int index, int numberOfElementsPerLine)
    {
        return index % numberOfElementsPerLine;
    }

    public static int GetLine(int index, int numberOfElementsPerLine)
    {
        return Mathf.CeilToInt(index / numberOfElementsPerLine);
    }

    public static float GetHorizontalPosition(Vector2 startPosition, Vector2 offset, int column)
    {
        return startPosition.x + offset.x * column;
    }

    public static float GetHorizontalPosition(int startHorizontalPosition, int offsetHorizontal, int column)
    {
        return startHorizontalPosition + offsetHorizontal * column;
    }

    public static float GetVerticalPosition(Vector2 startPosition, Vector2 offset, int line)
    {
        return startPosition.y + offset.y * line;
    }

    public static float GetVerticalPosition(int startVerticalPosition, int offsetVertical, int line)
    {
        return startVerticalPosition + offsetVertical * line;
    }


    //public static float GetVerticalPosition(Vector2 startPosition, Vector2 offset, int line)
    //{
    //    return startPosition.y + offset.y * line;
    //}
}