using UnityEngine;
using System.Collections;
using System;

public class PlayerResources : MonoBehaviour
{
    private ResourcePrerequisite[] resources;
    public Action PayDelegate;
    
    public PlayerResources()
    {
        this.resources = new ResourcePrerequisite[(int)EResourceCategory.Size];

        for (int resourceIndex = 0; resourceIndex < this.resources.Length; resourceIndex++)
            this.resources[resourceIndex]  = 
                (EResourceCategory.Gold == (EResourceCategory)resourceIndex) ?
                this.resources[resourceIndex] = new ResourcePrerequisite(12500, EResourceCategory.Gold) :
                this.resources[resourceIndex] = new ResourcePrerequisite(0, (EResourceCategory)resourceIndex);
    }

    public int GetResourceNumber(EResourceCategory resourceCategory)
    {
        return this.resources[(int)resourceCategory].ResourceNumber;
    }

    public void AddResource(EResourceCategory resourceCategory, int numberOfResource)
    {
        this.resources[(int)resourceCategory].ResourceNumber += numberOfResource;
    }

    public void RemoveResource(EResourceCategory resourceCategory, int numberOfResource)
    {
        this.resources[(int)resourceCategory].ResourceNumber -= numberOfResource;
    }

    public bool HaveEnoughtResource(ResourcePrerequisite[] resourcesNeed)
    {
        for (byte resourceIndex = 0; resourceIndex < resourcesNeed.Length; resourceIndex++)
        {
            if (this.resources[(int)(resourcesNeed[resourceIndex].ResourceCategory)].ResourceNumber <= resourcesNeed[resourceIndex].ResourceNumber)
                return false;
        }

        return true;
    }

    public bool Pay(ResourcePrerequisite[] resourcesNeed)
    {
        bool haveEnoughResource = this.HaveEnoughtResource(resourcesNeed);

        if (haveEnoughResource)
        {
             for (byte resourceIndex = 0; resourceIndex < resourcesNeed.Length; resourceIndex++)
                this.resources[(int)(resourcesNeed[resourceIndex].ResourceCategory)].ResourceNumber -= resourcesNeed[resourceIndex].ResourceNumber;

             if (null != this.PayDelegate)
                 this.PayDelegate();
        }

        return haveEnoughResource;
    }

    public void SubscribeToResourcesModificationDelegate(EResourceCategory resourceCategory, Action action)
    {
        this.resources[(int)resourceCategory].UpdateResourceNumberDelegate += action;

        this.resources[(int)resourceCategory].CallDelegate();
    }
}
