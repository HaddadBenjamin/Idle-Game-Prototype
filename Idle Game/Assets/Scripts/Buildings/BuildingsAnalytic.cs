using UnityEngine;
using System.Collections;

/// <summary>
/// Permet d'obtenir des informations sur le nombre de bâtiment posé sur le terrain en fonction de chaque type de bâtiment, bâtiment.
/// </summary>
public class BuildingsAnalytic
{
    #region Fields
    /// <summary>
    /// 
    /// </summary>
    private BuildingAnalytic[] constructionBuildingsThatHaveBeenBuild;
    private BuildingAnalytic piecesOfFurniture; // meubles
    #endregion

    #region Properties
    public BuildingAnalytic PiecesOfFurniture
    {
        get { return piecesOfFurniture; }
        private set { piecesOfFurniture = value; }
    }
    #endregion

    #region Constructor
    public BuildingsAnalytic()
    {
        int enumLength = EnumHelper.Count<EIndustryBuildingCategory>();

        this.constructionBuildingsThatHaveBeenBuild = new BuildingAnalytic[enumLength];

        for (int index = 0; index < enumLength; index++)
            this.constructionBuildingsThatHaveBeenBuild[index] = new BuildingAnalytic();

        this.piecesOfFurniture = new BuildingAnalytic(10);
    }
    #endregion

    #region Behaviour
    /// <summary>
    /// Récupère un BuildingAnalytic en fonction de son énumération.
    /// </summary>
    /// <param name="constructionCategory"></param>
    /// <returns></returns>
    public BuildingAnalytic GetConstructionBuildings(EIndustryBuildingCategory constructionCategory)
    {
        return this.constructionBuildingsThatHaveBeenBuild[EnumHelper.GetIndex<EIndustryBuildingCategory>(constructionCategory)];
    }

    /// <summary>
    /// Permet de mettre à jour les valeurs des buildingAnalytic.
    /// </summary>
    public void AtStartUpdateAllMembersSubscribeToDelegateAfterInitialization()
    {
        for (int index = 0; index < EnumHelper.Count<EIndustryBuildingCategory>(); index++)
            this.constructionBuildingsThatHaveBeenBuild[index].CallDelegate();

        this.piecesOfFurniture.CallDelegate();
    }
    #endregion
}
