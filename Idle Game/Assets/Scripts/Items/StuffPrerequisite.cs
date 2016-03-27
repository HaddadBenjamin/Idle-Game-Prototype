using UnityEngine;

[System.Serializable]
public class StuffPrerequisite
{
    #region Fields
    [SerializeField]
    private string name;
    [SerializeField]
    private int number;
    [SerializeField]
    private EStuffQuality quality;
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
        private set { number = value; }
    }

    public EStuffQuality Quality
    {
        get { return quality; }
        private set { quality = value; }
    }
    #endregion
}