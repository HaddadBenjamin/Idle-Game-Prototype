using UnityEngine;
using System.Collections;
using System;

[System.Serializable]
public class ResourcePrerequisite
{
    [SerializeField] // C'est aussi le nombre de resource prérequites lorsque l'on souhaite construire un batiment
    private int resourceNumber;
    [SerializeField]
    private EResourceCategory resourceCategory;

    public Action UpdateResourceNumberDelegate;

    public int ResourceNumber
    {
        get { return resourceNumber; }
        set
        { 
            resourceNumber = value;

            CallDelegate();
        }
    }

    public EResourceCategory ResourceCategory
    {
        get { return resourceCategory; }
        set { resourceCategory = value; }
    }

    public void CallDelegate()
    {
        if (null != this.UpdateResourceNumberDelegate)
            this.UpdateResourceNumberDelegate(); 
        // C'est dégeulasse, normalement on devrait utiliser une delegate qui prend en paramètre le nombre de resource.
    }
}
