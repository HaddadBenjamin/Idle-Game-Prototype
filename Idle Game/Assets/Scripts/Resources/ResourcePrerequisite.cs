using UnityEngine;
using System.Collections;

[System.Serializable]
public class ResourcePrerequisite
{
    [SerializeField] // C'est aussi le nombre de resource prérequites lorsque l'on souhaite construire un batiment
    private int resourceNumber;
    [SerializeField]
    private EResourceCategory resourceCategory;

    public int ResourceNumber
    {
        get { return resourceNumber; }
        set { resourceNumber = value; }
    }

    public EResourceCategory ResourceCategory
    {
        get { return resourceCategory; }
        set { resourceCategory = value; }
    }
}
