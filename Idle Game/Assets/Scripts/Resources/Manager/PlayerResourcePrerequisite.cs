[System.Serializable]
public class PlayerResourcePrerequisite : ResourcePrerequisite
{
    public PlayerResourcePrerequisite(int resourceNumber, EResourceCategory resourceCategory)
        : base(resourceNumber, resourceCategory)
    {
    }

    public void InitializeAtStart()
    {
        ServiceLocator.Instance.EventManagerResourceGenerated.SubcribeToEvent(base.ResourceCategory, base.AddResource);
    }

    protected override void ResourceNumberHaveBeenUpdated()
    {
        ServiceLocator.Instance.EventManagerResourceNumberHaveBeenUpdated.CallEvent(base.resourceCategory, base.ResourceNumber);
    }
}