using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public abstract class ABuildingParameters : MonoBehaviour
{
    #region Fields
    [SerializeField]
    private byte horizontalLength;
    [SerializeField]
    private byte verticalLength;
    [SerializeField]
    private float horizontalPercentageLength;
    [SerializeField]
    private float verticalPercentageLength;
    [SerializeField]
    private string buildingName;
    // PriceEquivalent
    [SerializeField]
    private ResourcePrerequisite[] resourcesPrerequisiteToBuildThisBuilding;
    //private List<Pre
    //private long price;
    [SerializeField]
    private EBuildingCategory buildingCategory;
    #endregion

    #region Properties
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
    public string BuildingName
    {
        get { return buildingName; }
        private set { buildingName = value; }
    }
    //public long Price
    //{
    //    get { return price; }
    //    private set { price = value; }
    //}
    public EBuildingCategory BuildingCategory
    {
        get { return buildingCategory; }
        private set { buildingCategory = value; }
    }

    public ResourcePrerequisite[] ResourcesPrerequisiteToBuildThisBuilding
    {
        get { return resourcesPrerequisiteToBuildThisBuilding; }
        private set { resourcesPrerequisiteToBuildThisBuilding = value; }
    }
    #endregion
}
