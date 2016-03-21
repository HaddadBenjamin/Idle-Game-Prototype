using UnityEngine;
using System.Collections;

public class IndustryBuilding : ABuilding
{
    /// <summary>
    /// Type d'industrie.
    /// </summary>
    [SerializeField]
    private EIndustryBuildingCategory constructionBuildingCategory;

    public EIndustryBuildingCategory ConstructionBuildingCategory
    {
        get { return constructionBuildingCategory; }
        private set { constructionBuildingCategory = value; }
    }
}
