[System.Serializable]
public class PlayerResourcePrerequisite : ResourcePrerequisite
{
    public PlayerResourcePrerequisite(int resourceNumber, EResourceCategory resourceCategory)
        : base(resourceNumber, resourceCategory)
    {
    }

    public void InitializeAtStart()
    {
        ServiceContainer.Instance.EventManagerResourceGenerated.SubcribeToEvent(base.ResourceCategory, base.AddResource);
    }

    protected override void ResourceNumberHaveBeenUpdated()
    {
        ServiceContainer.Instance.EventManagerResourceNumberHaveBeenUpdated.CallEvent(base.resourceCategory, base.ResourceNumber);
    }
}