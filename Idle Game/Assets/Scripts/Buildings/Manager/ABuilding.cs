using UnityEngine;
using System.Collections;
using System.Collections.Generic;

/// <summary>
/// Paramètre de placement du bâtiment.
/// </summary>
public abstract class ABuilding : MonoBehaviour
{
    #region Fields
    /// <summary>
    /// Type de bâtiment : construction, décors, autre ??e
    /// </summary>
    [SerializeField]
    private EBuildingCategory buildingCategory;
    [SerializeField]
    private string buildingName;
    private ConstructionSquare constructionSquareReference;
    public int BuildingLevel { get; protected set; }
    #endregion

    #region Properties
    public EBuildingCategory BuildingCategory
    {
        get { return buildingCategory; }
        private set { buildingCategory = value; }
    }

    public string BuildingName
    {
        get { return buildingName; }
        set { buildingName = value; }
    }

    public ConstructionSquare ConstructionSquareReference
    {
        get { return constructionSquareReference; }
        set { constructionSquareReference = value; }
    }
    #endregion


    #region Behaviour Methods
    public void Sell()
    {
        PlayerResources         playerResources =  ServiceContainer.Instance.
                                GameObjectReferenceManager.Get("[PLAYER]").
                                GetComponent<PlayerResources>();
        BuildingConfiguration   buildingConfiguration = ServiceContainer.Instance.BuildingsConfiguration.GetConfiguration(this.BuildingName);

        for (int buildingLevelIndex = 1; buildingLevelIndex <= this.BuildingLevel; buildingLevelIndex++)
            playerResources.Unpay(buildingConfiguration.GetLevelConfigurationIfPossible(buildingLevelIndex).Price);
    }

    public ResourcePrerequisite[] GetSellPrice()
    {
        ResourcePrerequisite[] price = ServiceContainer.Instance.BuildingsConfiguration.GetConfiguration(this.BuildingName).GetLevelConfigurationIfPossible(1).Price;

        for (int level = 1; level < this.BuildingLevel; level++)
            price = ResourceHelper.Add(price, ServiceContainer.Instance.BuildingsConfiguration.GetConfiguration(this.BuildingName).GetLevelConfigurationIfPossible(level + 1).Price);

        return price;
    }

    public ResourcePrerequisite[] GetPriceToLevelUp()
    {
        BuildingLevelsConfiguration levelConfiguration = ServiceContainer.Instance.BuildingsConfiguration.GetConfiguration(this.BuildingName).GetLevelConfigurationIfPossible(this.BuildingLevel + 1);

        return (null != levelConfiguration) ?
                levelConfiguration.Price :
                null;
    }
    #endregion
}
