using UnityEngine;
using System.Collections;

public class BuildingParameters : MonoBehaviour
{
    [SerializeField]
    private byte horizontalLength;
    [SerializeField]
    private byte verticalLength;
    [SerializeField]
    public float horizontalPercentageLength;
    [SerializeField]
    public float verticalPercentageLength;

    public byte HorizontalLenght
    { 
        get { return horizontalLength; }
        private set { horizontalLength = value; } 
    }
    public byte VerticalLenght
    {
        get { return verticalLength; }
        private set { verticalLength = value; } 
    }

    public float HorizontalPercentageLength
    {
        get { return horizontalPercentageLength; }
        private set { horizontalPercentageLength = value; }
    }
    public float VerticalPercentageLength
    {
        get { return verticalPercentageLength; }
        private set { verticalPercentageLength = value; }
    }
}
