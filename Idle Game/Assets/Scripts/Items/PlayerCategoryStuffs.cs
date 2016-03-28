using System.Collections.Generic;

public class PlayerCategoryStuffs
{
    #region Fields
    private List<PlayerStuffPrerequisite> stuffsFromCategory;
    #endregion

    #region Constructor
    public PlayerCategoryStuffs()
    {
        this.stuffsFromCategory = new List<PlayerStuffPrerequisite>();
    }
    #endregion

    #region Behaviour Methods
    public PlayerStuffPrerequisite Get(EStuffQuality quality, string name)
    {
        return this.stuffsFromCategory.Find(stuff => stuff.Quality == quality && stuff.Name == name);
    }

    public int GetNumber(EStuffQuality quality, string name)
    {
        PlayerStuffPrerequisite stuffPrerequisite = this.Get(quality, name);

        return
            null != stuffPrerequisite ?
            stuffPrerequisite.Number :
            0;
    }

    public void AddNumber(string name, EStuffCategory stuffCategory, EStuffQuality stuffQuality, int numberOfStuff)
    {
        PlayerStuffPrerequisite stuffPrerequisite = this.Get(stuffQuality, name);

        if (null != stuffPrerequisite)
            stuffPrerequisite.AddStuff(numberOfStuff);
        else
            this.stuffsFromCategory.Add(new PlayerStuffPrerequisite(name, numberOfStuff, stuffCategory, stuffQuality));
    }

    public void RemoveNumber(string name, EStuffCategory stuffCategory, EStuffQuality stuffQuality, int numberOfStuff)
    {
        PlayerStuffPrerequisite stuffPrerequisite = this.Get(stuffQuality, name);

        if (null != stuffPrerequisite)
            stuffPrerequisite.RemoveStuff(numberOfStuff);
        else
            this.stuffsFromCategory.Remove(new PlayerStuffPrerequisite(name, numberOfStuff, stuffCategory, stuffQuality));
    }

    public bool HaveEnoughtStuff(StuffPrerequisite stuff)
    {
        PlayerStuffPrerequisite stuffPrerequisite = this.Get(stuff.Quality, stuff.Name);

        return
            null == stuffPrerequisite ?
            false :
            stuffPrerequisite.Number >= stuff.Number;
    }

    public bool PayIfPossible(StuffPrerequisite stuff)
    {
        bool haveEnoughtStuffToPay = this.HaveEnoughtStuff(stuff);

        if (haveEnoughtStuffToPay)
            this.Get(stuff.Quality, stuff.Name).RemoveStuff(stuff.Number);

        return haveEnoughtStuffToPay;
    }

    public void Unpay(StuffPrerequisite stuff)
    {
        PlayerStuffPrerequisite stuffPrerequisite = this.Get(stuff.Quality, stuff.Name);

        if (null != stuffPrerequisite)
            stuffPrerequisite.AddStuff(stuff.Number);
    }

    #endregion
}