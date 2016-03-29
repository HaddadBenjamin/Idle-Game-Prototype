public class PlayerStuffPrerequisite : StuffPrerequisite
{
    public PlayerStuffPrerequisite(string name, int number, EStuffCategory stuffCategory, EStuffQuality quality)
        : base(name, number, stuffCategory, quality)
    {
    }

    protected override void StuffNumberHaveBeenUpdated()
    {
        ServiceContainer.Instance.EventManagerStuffNumberHaveBeenUpdated.CallEvent(base.StuffCategory, base.Quality, base.Number, base.Name);
    }
}