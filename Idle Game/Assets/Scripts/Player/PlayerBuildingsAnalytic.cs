using UnityEngine;
using System.Collections;

public class PlayerBuildingsAnalytic
{
    private int[] constructionBuildingsThatHaveBeenBuild;
    public Event ConstructionBuildingsThatHaveBeenBuildDelegate;

    public PlayerBuildingsAnalytic()
    {
        this.constructionBuildingsThatHaveBeenBuild = new int[(int)EConstructionBuildingCategory.Size];
    }

    public void AddConstructionBuilding(EConstructionBuildingCategory constructionCategory)
    {
        ++this.constructionBuildingsThatHaveBeenBuild[(int)constructionCategory];
       
        this.CallConstructionBuildingsThatHaveBeenBuildDelegate();
    }

    public void RemoveConstructionBuilding(EConstructionBuildingCategory constructionCategory)
    {
        --this.constructionBuildingsThatHaveBeenBuild[(int)constructionCategory];

        this.CallConstructionBuildingsThatHaveBeenBuildDelegate();
    }

    public int GetNumberOfConstructionBuilding(EConstructionBuildingCategory constructionCategory)
    {
        return this.constructionBuildingsThatHaveBeenBuild[(int)constructionCategory];
    }

    private void CallConstructionBuildingsThatHaveBeenBuildDelegate()
    {
        if (null != this.ConstructionBuildingsThatHaveBeenBuildDelegate)
            this.CallConstructionBuildingsThatHaveBeenBuildDelegate();
    }
}
