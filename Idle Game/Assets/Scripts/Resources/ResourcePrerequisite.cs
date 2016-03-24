using UnityEngine;
using System.Collections;
using System;

[System.Serializable]
public class ResourcePrerequisite
{
    #region Fields
    [SerializeField] // C'est aussi le nombre de resource prérequites lorsque l'on souhaite construire un batiment
    protected int resourceNumber;
    [SerializeField]
    protected EResourceCategory resourceCategory;
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
        private set
        { 
            resourceNumber = value;

            this.ResourceNumberHaveBeenUpdated();
        }
    }

    public EResourceCategory ResourceCategory
    {
        get { return resourceCategory; }
        private set { resourceCategory = value; }
    }
    #endregion

    #region Virtual & Abstract Methods
    protected virtual void ResourceNumberHaveBeenUpdated() { }
    #endregion

    #region Behaviour Methods
    public void AddResource(int resourceAdded)
    {
        this.ResourceNumber += resourceAdded;
    }

    public void RemoveResource(int resourceRemoved)
    {
        this.ResourceNumber -= resourceRemoved;
    }
    #endregion

}
