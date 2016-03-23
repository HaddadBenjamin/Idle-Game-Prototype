using UnityEngine;
using System.Collections;
using System;

public class PlayerResources : MonoBehaviour
{
    #region Fields
    private ResourcePrerequisite[] resources;
    #endregion

    #region Constructor
    public PlayerResources()
    {
        this.resources = new ResourcePrerequisite[EnumHelper.Count<EResourceCategory>()];

        for (int resourceIndex = 0; resourceIndex < this.resources.Length; resourceIndex++)
            this.resources[resourceIndex]  = 
                (EResourceCategory.Gold == (EResourceCategory)resourceIndex) ?
                this.resources[resourceIndex] = new ResourcePrerequisite(12500, EResourceCategory.Gold) :
                this.resources[resourceIndex] = new ResourcePrerequisite(0, (EResourceCategory)resourceIndex);
    }
    #endregion

    #region Behaviour Methods
    /// <summary>
    /// Récupère le nombre de resource du type resourceCategory.
    /// </summary>
    /// <param name="resourceCategory"></param>
    /// <returns></returns>
    public int GetResourceNumber(EResourceCategory resourceCategory)
    {
        return this.resources[EnumHelper.GetIndex<EResourceCategory>(resourceCategory)].ResourceNumber;
    }

    /// <summary>
    /// Permet de rajouter numbrOfResource à la resource ayant pour identifiant resourceCategory.
    /// </summary>
    /// <param name="resourceCategory"></param>
    /// <param name="numberOfResource"></param>
    public void AddResource(EResourceCategory resourceCategory, int numberOfResource)
    {
        this.resources[EnumHelper.GetIndex<EResourceCategory>(resourceCategory)].ResourceNumber += numberOfResource;
    }

    /// <summary>
    /// Réduire de numberOfResource à la resource aynat pour identifiant resourceCategory.
    /// </summary>
    /// <param name="resourceCategory"></param>
    /// <param name="numberOfResource"></param>
    public void RemoveResource(EResourceCategory resourceCategory, int numberOfResource)
    {
        this.resources[EnumHelper.GetIndex<EResourceCategory>(resourceCategory)].ResourceNumber -= numberOfResource;
    }

    /// <summary>
    /// Détermine si vous possédez suffisament de resources pour payer resourcesNeed.
    /// </summary>
    /// <param name="resourcesNeed"></param>
    /// <returns></returns>
    public bool HaveEnoughtResource(ResourcePrerequisite[] resourcesNeed)
    {
        for (byte resourceIndex = 0; resourceIndex < resourcesNeed.Length; resourceIndex++)
        {
            if (this.resources[EnumHelper.GetIndex<EResourceCategory>(resourcesNeed[resourceIndex].ResourceCategory)].ResourceNumber 
                <= resourcesNeed[resourceIndex].ResourceNumber)
                return false;
        }

        return true;
    }

    /// <summary>
    /// Paye resourceNeed si vous possédez suffisament de resources et renvoi si vous avez pu payer.
    /// </summary>
    /// <param name="resourcesNeed"></param>
    /// <returns></returns>
    public bool Pay(ResourcePrerequisite[] resourcesNeed)
    {
        bool haveEnoughResource = this.HaveEnoughtResource(resourcesNeed);

        if (haveEnoughResource)
        {
             for (byte resourceIndex = 0; resourceIndex < resourcesNeed.Length; resourceIndex++)
                this.resources[EnumHelper.GetIndex<EResourceCategory>(resourcesNeed[resourceIndex].ResourceCategory)].ResourceNumber 
                    -= resourcesNeed[resourceIndex].ResourceNumber;

             ServiceLocator.Instance.EventManager.CallEvent(EEvent.PlayerPayResources);
        }

        return haveEnoughResource;
    }
    #endregion
}
