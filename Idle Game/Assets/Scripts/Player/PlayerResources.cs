using UnityEngine;
using System.Collections;

public class PlayerResources : MonoBehaviour
{
    [SerializeField]
    private ResourcePrerequisite[] resources;

    void Awake()
    {
        this.resources = new  ResourcePrerequisite[(int)EResourceCategory.Size];

        this.resources[(int)EResourceCategory.Gold].ResourceNumber = 5000;
    }

    public void AddResource(EResourceCategory resourceCategory, int numberOfResource)
    {
        this.resources[(int)resourceCategory].ResourceNumber += numberOfResource;
    }

    public void RemoveResource(EResourceCategory resourceCategory, int numberOfResource)
    {
        this.resources[(int)resourceCategory].ResourceNumber -= numberOfResource;
    }

    private bool HaveEnoughtResource(ResourcePrerequisite[] resourcesNeed)
    {
        for (byte resourceIndex = 0; resourceIndex < resourcesNeed.Length; resourceIndex++)
        {
            if (this.resources[(int)(resourcesNeed[resourceIndex].ResourceCategory)].ResourceNumber >= resourcesNeed[resourceIndex].ResourceNumber)
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
        }

        return haveEnoughResource;
    }
}
