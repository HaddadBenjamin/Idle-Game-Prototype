using UnityEngine;
using System.Collections;

public class ConstructionBuildingParameters : BuildingPlacement
{
    [SerializeField]
    private EConstructionBuildingCategory constructionBuildingCategory;

    public EConstructionBuildingCategory ConstructionBuildingCategory
    {
        get { return constructionBuildingCategory; }
        private set { constructionBuildingCategory = value; }
    }
}
