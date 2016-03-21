using UnityEngine;
using System.Collections;

public class PlayerBuildingsAnalytic
{
    #region Fields
    private MinimumMaximumBuilding[] constructionBuildingsThatHaveBeenBuild;
    private MinimumMaximumBuilding piecesOfFurniture; // meubles
    #endregion

    #region Properties
    public MinimumMaximumBuilding PiecesOfFurniture
    {
        get { return piecesOfFurniture; }
        private set { piecesOfFurniture = value; }
    }
    #endregion

    #region Constructor
    public PlayerBuildingsAnalytic()
    {
        this.constructionBuildingsThatHaveBeenBuild = new MinimumMaximumBuilding[(int)EIndustryBuildingCategory.Size];

        for (int index = 0; index < (int)EIndustryBuildingCategory.Size; index++)
            this.constructionBuildingsThatHaveBeenBuild[index] = new MinimumMaximumBuilding();

        this.piecesOfFurniture = new MinimumMaximumBuilding(10);
    }
    #endregion

    #region Behaviour
    public MinimumMaximumBuilding GetConstructionBuildings(EIndustryBuildingCategory constructionCategory)
    {
        return this.constructionBuildingsThatHaveBeenBuild[(int)constructionCategory];
    }

    public void FirstUpdateAllMembersSubscribeToDelegateAfterInitialization()
    {
        for (int index = 0; index < (int)EIndustryBuildingCategory.Size; index++)
            this.constructionBuildingsThatHaveBeenBuild[index].CallDelegate();

        this.piecesOfFurniture.CallDelegate();
    }
    #endregion
}
