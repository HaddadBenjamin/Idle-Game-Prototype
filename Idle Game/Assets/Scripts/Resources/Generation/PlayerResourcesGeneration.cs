using UnityEngine;

public class PlayerResourcesGeneration
{
    #region Fields & Properties
    public EResourceCategory ResourceCategory { get; private set; }
    public float ResourceGeneratedPerSeconds { get; private set; }
    public float TotalResourceGenerated { get; private set; }
    public float ResourceGenerated { get; private set; }
    #endregion

    #region Constructor
    public PlayerResourcesGeneration(EResourceCategory resourceCategory, float resourceGeneratedPerSeconds = 0.0f)
    {
        this.ResourceCategory = resourceCategory;
        this.ResourceGeneratedPerSeconds = resourceGeneratedPerSeconds;
    }
    #endregion

    #region Behaviour Methods
    public void AddResourceGeneratedPerSeconds(float resourceAdded)
    {
        this.ResourceGeneratedPerSeconds += resourceAdded;
    }

    public void RemoveResourceGeneratedPerSeconds(float resourceRemoved)
    {
        this.ResourceGeneratedPerSeconds -= resourceRemoved;
    }

    public void GenerateResources()
    {
        this.ResourceGenerated += this.ResourceGeneratedPerSeconds;
        this.TotalResourceGenerated += this.ResourceGeneratedPerSeconds;

        if (this.ResourceGenerated >= 1.0f)
        {
            int resourceGeneratedAsInt = Mathf.CeilToInt(this.ResourceGenerated);

            ServiceLocator.Instance.EventManagerResourceGenerated.CallEvent(this.ResourceCategory, resourceGeneratedAsInt);

            this.ResourceGenerated -= Mathf.Ceil(this.ResourceGenerated);
        }
    }
    #endregion
}