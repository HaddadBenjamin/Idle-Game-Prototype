using UnityEngine;

[System.Serializable]
public class BuildingLevelsConfiguration
{
    #region Fields
    [SerializeField]
    private BuildingLevelResourceGenerationConfiguration[] resourceGeneration;
    [SerializeField]
    private ResourcePrerequisite[] priceToLevelUp;
    #endregion

    #region Properties
    public BuildingLevelResourceGenerationConfiguration[] ResourceGeneration
    {
        get { return resourceGeneration; }
        private set { resourceGeneration = value; }
    }

    public ResourcePrerequisite[] PriceToLevelUp
    {
        get { return priceToLevelUp; }
        private set { priceToLevelUp = value; }
    }
    #endregion
}