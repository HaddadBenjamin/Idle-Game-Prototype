using UnityEngine;
using System.Collections;

public class GridPosition
{
    #region Fields
    private int horizontalGridPosition;
    private int verticalGridPosition;
    #endregion

    public GridPosition(int x, int y)
    {
        this.horizontalGridPosition = x;
        this.verticalGridPosition = y;
    }

    #region Properties
    public int HorizontalGridPosition
    {
        get { return horizontalGridPosition; }
        private set { horizontalGridPosition = value; }
    }

    public int VerticalGridPosition
    {
        get { return verticalGridPosition; }
        private set { verticalGridPosition = value; }
    }
    #endregion
}
