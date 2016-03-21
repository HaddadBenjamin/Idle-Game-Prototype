using UnityEngine;
using System.Collections;

public class GridPosition
{
    #region Fields
    private int positionX;
    private int positionY;
    #endregion

    public GridPosition(int x, int y)
    {
        this.positionX = x;
        this.positionY = y;
    }

    #region Properties
    public int PositionX
    {
        get { return positionX; }
        set { positionX = value; }
    }

    public int PositionY
    {
        get { return positionY; }
        set { positionY = value; }
    }
    #endregion
}
