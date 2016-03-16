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
    private float horizontalOffsetNormalized;
    [SerializeField]
    private float verticalOffsetNormalized;
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
    public float HorizontalOffsetNormalized
    {
        get { return horizontalOffsetNormalized; }
        private set { horizontalOffsetNormalized = value; }
    }
    public float VerticalOffsetNormalized
    {
        get { return verticalOffsetNormalized; }
        private set { verticalOffsetNormalized = value; }
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

    /// <summary>
    /// Permet de placer le bâtiment de façon qu'il ne dépasse pas sur les côté.
    /// </summary>
    /// <param name="horizontal"></param>
    /// <returns></returns>
    public int GetBuildingHorizontal(int horizontal)
    {
        if (horizontal < this.HorizontalLenght - 1)
            horizontal = this.HorizontalLenght - 1;

        return horizontal;
    }

    public int GetBuildingVertical(int vertical)
    {
        if (vertical < this.VerticalLenght - 1)
            vertical = this.VerticalLenght - 1;

        return vertical;
    }
}
