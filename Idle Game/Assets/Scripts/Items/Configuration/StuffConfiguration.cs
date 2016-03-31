using UnityEngine;

[System.Serializable]
public class StuffConfiguration
{
    #region Fields
    [SerializeField]
    private string stuffName;
    private EStuffCategory stuffCategory;
    [SerializeField]
    private int stuffLevel;
    [SerializeField]
    private int basePower;
    [SerializeField]
    private int basePrice;
    [SerializeField]
    private int experienceGain;
    [SerializeField]
    private int timeToCraft;
    [SerializeField]
    private ResourcePrerequisite[] resourcesPrerequisite;
    [SerializeField]
    private StuffPrerequisite[] stuffsPrerequisite;
    [SerializeField]
    private RawPrerequisite[] rawsPrerequisite; 
    #endregion

    #region Properties
    public string StuffName
    {
        get { return stuffName; }
        private set { stuffName = value; }
    }

    public EStuffCategory StuffCategory
    {
        get { return stuffCategory; }
        private set { stuffCategory = value; }
    }

    public int StuffLevel
    {
        get { return stuffLevel; }
        private set { stuffLevel = value; }
    }

    public int BasePower
    {
        get { return basePower; }
        private set { basePower = value; }
    }

    public ResourcePrerequisite[] ResourcesPrerequisite
    {
        get { return resourcesPrerequisite; }
        private set { resourcesPrerequisite = value; }
    }

    public StuffPrerequisite[] StuffsPrerequisite
    {
        get { return stuffsPrerequisite; }
        private set { stuffsPrerequisite = value; }
    }

    public RawPrerequisite[] RawsPrerequisite
    {
        get { return rawsPrerequisite; }
        private set { rawsPrerequisite = value; }
    }

    public int TimeToCraft
    {
        get { return timeToCraft; }
        private set { timeToCraft = value; }
    }
    #endregion

    #region Behaviour Methods
    public void InitializeStuffCategory(EStuffCategory stuffCategory)
    {
        this.stuffCategory = stuffCategory;
    }
    #endregion
}