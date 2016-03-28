using UnityEngine;

[System.Serializable]
public class RawPrerequisite
{
    #region Fields
    [SerializeField]
    private ERaw rawCategory;
    [SerializeField]
    private int number;
    #endregion

     #region Constructor
    public RawPrerequisite(int rawNumber, ERaw rawCategory)
    {
        this.number = rawNumber;
        this.rawCategory = rawCategory;
    }
    #endregion

    #region Properties
    public ERaw RawCategory
    {
        get { return rawCategory; }
        private set { rawCategory = value; }
    }

    public int Number
    {
        get { return number; }
        private set
        { 
            number = value;

            this.RawNumberHaveBeenUpdated();
        }
    }
    #endregion

    #region Virtual & Abstract Methods
    protected virtual void RawNumberHaveBeenUpdated() { }
    #endregion

    #region Behaviour Methods
    public void AddRaw(int rawAdded)
    {
        this.Number += rawAdded;
    }

    public void RemoveRaw(int rawRemoved)
    {
        this.Number -= rawRemoved;
    }
    #endregion
}