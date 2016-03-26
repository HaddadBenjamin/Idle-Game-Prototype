using UnityEngine;

[System.Serializable]
public class BuildingLevelsConfiguration
{
    #region Fields
    [SerializeField]
    private BuildingLevelResourceGenerationConfiguration[] resourceGeneration;
    [SerializeField]
    private ResourcePrerequisite[] price;
    #endregion

    #region Properties
    public BuildingLevelResourceGenerationConfiguration[] ResourceGeneration
    {
        get { return resourceGeneration; }
        private set { resourceGeneration = value; }
    }

    public ResourcePrerequisite[] Price
    {
        get { return price; }
        private set { price = value; }
    }
    #endregion
}