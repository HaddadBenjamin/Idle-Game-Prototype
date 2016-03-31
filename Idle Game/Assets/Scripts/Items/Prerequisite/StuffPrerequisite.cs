using UnityEngine;

[System.Serializable]
public class StuffPrerequisite
{
    #region Fields
    [SerializeField]
    protected string name;
    [SerializeField]
    protected int number;
    [SerializeField]
    protected EStuffCategory stuffCategory;
    [SerializeField]
    protected EStuffQuality quality;
    #endregion

    #region Constructor
    public StuffPrerequisite(string name, int number, EStuffCategory stuffCategory, EStuffQuality quality)
    {
        this.name = name;
        this.number = number;
        this.stuffCategory = stuffCategory;
        this.quality = quality;

        this.StuffNumberHaveBeenUpdated();
    }
    #endregion

    #region Properties
    public string Name
    {
        get { return name; }
        private set { name = value; }
    }

    public int Number
    {
        get { return number; }
        private set 
        { 
            number = value;

            this.StuffNumberHaveBeenUpdated();
        }
    }

    public EStuffCategory StuffCategory
    {
        get { return stuffCategory; }
        private set { stuffCategory = value; }
    }

    public EStuffQuality Quality
    {
        get { return quality; }
        private set { quality = value; }
    }
    #endregion

    #region Virtual & Abstract Methods
    protected virtual void StuffNumberHaveBeenUpdated() { }
    #endregion

    #region Behaviour Methods
    public void AddStuff(int stuffAdded)
    {
        this.Number += stuffAdded;
        //this.StuffNumberHaveBeenUpdated();
    }

    public void RemoveStuff(int stuffRemoved)
    {
        this.Number -= stuffRemoved;
        //this.StuffNumberHaveBeenUpdated();
    }
    #endregion
}