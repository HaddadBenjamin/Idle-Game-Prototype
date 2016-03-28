[System.Serializable]
public class PlayerRawPrerequisite : RawPrerequisite
{
    public PlayerRawPrerequisite(int resourceNumber, ERaw rawCategory)
        : base(resourceNumber, rawCategory)
    {
    }

    protected override void RawNumberHaveBeenUpdated()
    {
        ServiceLocator.Instance.EventManagerRawNumberHaveBeenUpdated.CallEvent(base.RawCategory, base.Number);
    }
}