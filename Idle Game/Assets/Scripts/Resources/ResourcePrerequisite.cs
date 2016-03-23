using UnityEngine;
using System.Collections;
using System;

[System.Serializable]
public class ResourcePrerequisite
{
    #region Fields
    [SerializeField] // C'est aussi le nombre de resource prérequites lorsque l'on souhaite construire un batiment
    private int resourceNumber;
    [SerializeField]
    private EResourceCategory resourceCategory;
    #endregion

    #region Constructor
    public ResourcePrerequisite(int resourceNumber, EResourceCategory resourceCategory)
    {
        this.resourceNumber = resourceNumber;
        this.resourceCategory = resourceCategory;
    }
    #endregion

    #region Properties
    public int ResourceNumber
    {
        get { return resourceNumber; }
        set
        { 
            resourceNumber = value;

            ServiceLocator.Instance.EventManagerEResourceCategory.CallEvent(this.resourceCategory);
        }
    }

    public EResourceCategory ResourceCategory
    {
        get { return resourceCategory; }
        private set { resourceCategory = value; }
    }
    #endregion

    #region Behaviour Methods
    #endregion

}
