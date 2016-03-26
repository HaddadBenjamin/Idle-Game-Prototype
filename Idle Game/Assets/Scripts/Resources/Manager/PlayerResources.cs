using UnityEngine;
using System.Collections;
using System;

public class PlayerResources : MonoBehaviour
{
    #region Fields
    private PlayerResourcePrerequisite[] resources;
    private PlayerResourcesGeneration[] resourcesGeneration;
    private Alarm generationResourceTimer = new Alarm(1.0f, false);
    #endregion

    #region Constructor
    public PlayerResources()
    {
        int enumerationResourceCategorySize = EnumHelper.Count<EResourceCategory>();

        this.resources = new PlayerResourcePrerequisite[enumerationResourceCategorySize];
        this.resourcesGeneration = new PlayerResourcesGeneration[enumerationResourceCategorySize];

        for (int resourceIndex = 0; resourceIndex < this.resources.Length; resourceIndex++)
        {
            if (EResourceCategory.Gold == (EResourceCategory)resourceIndex)
            {
                this.resources[resourceIndex] = new PlayerResourcePrerequisite(12500, (EResourceCategory)resourceIndex);
                this.resourcesGeneration[resourceIndex] = new PlayerResourcesGeneration((EResourceCategory)resourceIndex, 75.0f);
            }
            else
            {
                this.resources[resourceIndex] = new PlayerResourcePrerequisite(0, (EResourceCategory)resourceIndex);
                this.resourcesGeneration[resourceIndex] = new PlayerResourcesGeneration((EResourceCategory)resourceIndex);
            }
        }
    }
    #endregion

    #region Unity Methods
    void Start()
    {
        Array.ForEach(this.resources, resource => resource.InitializeAtStart());
    }

    void Update()
    {
        if (this.generationResourceTimer.IsRingingUpdated())
            Array.ForEach(this.resourcesGeneration, resourceGeneration => resourceGeneration.GenerateResources());
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
        this.resources[EnumHelper.GetIndex<EResourceCategory>(resourceCategory)].AddResource(numberOfResource);
    }

    /// <summary>
    /// Réduire de numberOfResource à la resource aynat pour identifiant resourceCategory.
    /// </summary>
    /// <param name="resourceCategory"></param>
    /// <param name="numberOfResource"></param>
    public void RemoveResource(EResourceCategory resourceCategory, int numberOfResource)
    {
        this.resources[EnumHelper.GetIndex<EResourceCategory>(resourceCategory)].RemoveResource(numberOfResource);
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
                this.resources[EnumHelper.GetIndex<EResourceCategory>(resourcesNeed[resourceIndex].ResourceCategory)].
                    RemoveResource(resourcesNeed[resourceIndex].ResourceNumber);

             ServiceLocator.Instance.EventManager.CallEvent(EEvent.PlayerPayResources);
        }

        return haveEnoughResource;
    }

    public void GenerateResource(BuildingLevelResourceGenerationConfiguration[] resourceGenerated)
    {
        for (int resourceGeneratedIndex = 0; resourceGeneratedIndex < resourceGenerated.Length; resourceGeneratedIndex++)
        {
            EResourceCategory resourceType = resourceGenerated[resourceGeneratedIndex].ResourceType;
            float resourceAdded = resourceGenerated[resourceGeneratedIndex].ResourceGeneratedPerSeconds;

            this.resourcesGeneration[EnumHelper.GetIndex<EResourceCategory>(resourceType)].
                AddResourceGeneratedPerSeconds(resourceAdded);
        }
    }
    #endregion
}
