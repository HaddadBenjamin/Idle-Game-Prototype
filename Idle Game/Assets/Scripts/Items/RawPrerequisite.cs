using UnityEngine;

public class RawPrerequisite : MonoBehaviour
{
    #region Fields
    [SerializeField]
    private ERaw rawCategory;
    [SerializeField]
    private int number;
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
        private set { number = value; }
    }
    #endregion

    #region Unity Methods
    #endregion

    #region Behaviour Methods
    #endregion
}