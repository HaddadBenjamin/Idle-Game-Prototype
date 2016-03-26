using UnityEngine;

[System.Serializable]
public class BuildingLevelResourceGenerationConfiguration
{
    #region Fields
    [SerializeField]
    private EResourceCategory resourceType;
    [SerializeField]
    private float resourceGeneratedPerSeconds;
    #endregion

    #region Constructor
    public BuildingLevelResourceGenerationConfiguration() { }

    public BuildingLevelResourceGenerationConfiguration(EResourceCategory resourceType, float resourceGeneratedPerSeconds)
    {
        this.resourceType = resourceType;
        this.resourceGeneratedPerSeconds = resourceGeneratedPerSeconds;
    }
    #endregion

    #region Properties
    public EResourceCategory ResourceType
    {
        get { return resourceType; }
        private set { resourceType = value; }
    }

    public float ResourceGeneratedPerSeconds
    {
        get { return resourceGeneratedPerSeconds; }
        private set { resourceGeneratedPerSeconds = value; }
    }
    #endregion
}