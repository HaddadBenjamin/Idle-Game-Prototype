using UnityEngine;
using System.Collections;

public class IndustryBuilding : ABuilding
{
    #region Fields & Some Properties
    /// <summary>
    /// Type d'industrie.
    /// </summary>
    [SerializeField]
    private EIndustryBuildingCategory constructionBuildingCategory;
    [SerializeField]
    private string buildingName;
    public BuildingLevelsConfiguration LevelsConfiguration { get; private set; }
    public BuildingConfiguration BuildingConfiguration { get; private set; }
    public int BuildingLevel { get; private set; }
    public PlayerResources playerResources { get; private set; }
    #endregion

    #region Properites
    public EIndustryBuildingCategory ConstructionBuildingCategory
    {
        get { return constructionBuildingCategory; }
        private set { constructionBuildingCategory = value; }
    }
    #endregion

    #region Initializer
    public void InitializeResourceGeneration(string buildingName)
    {
        this.buildingName = buildingName;
        this.BuildingLevel = 1;

        this.BuildingConfiguration = ServiceLocator.Instance.
                                    BuildingsConfiguration.GetConfiguration(this.buildingName);

        this.LevelsConfiguration = this.GetLevelsConfiguration(this.BuildingLevel);

        this.playerResources =  ServiceLocator.Instance.
                                GameObjectReferenceManager.Get("[PLAYER]").
                                GetComponent<PlayerResources>();

        this.playerResources.GenerateResource(this.LevelsConfiguration.ResourceGeneration);

        //this.LevelUp();
    }

    public bool CanLevelup()
    {
        if (this.BuildingLevel + 1 >= ServiceLocator.Instance.BuildingsConfiguration.GetConfiguration(this.buildingName).MaximumLevel)
        {
            if (this.playerResources.HaveEnoughtResource(this.LevelsConfiguration.Price))
                return true;
            else
                Debug.Log("You don't have enough resource to level up this building");
        }
        else
            Debug.Log("You can't level up this building because it level is already to its maximum");
        
        return false;
    }
    
    public void LevelUpIfPossible()
    {
        if (this.CanLevelup())
            this.LevelUp();
    }

    private void LevelUp()
    {
        this.playerResources.GenerateResource(
            this.GetLevelsDifferenceConfiguration(this.LevelsConfiguration.ResourceGeneration, this.BuildingLevel));

        ++this.BuildingLevel;

        this.LevelsConfiguration = this.GetLevelsConfiguration(this.BuildingLevel);

        this.playerResources.Pay(this.LevelsConfiguration.Price);

        //Action(level) ? pour les synergies ?
    }

    // A mettre dans un helper, DEVRA ABSOLUMENET TESTER SI LE NIVEAU DU BATIMENT PEUT LEVEL UP AVANT DETRE APPELER.
    private BuildingLevelResourceGenerationConfiguration[] GetLevelsDifferenceConfiguration(BuildingLevelResourceGenerationConfiguration[] A = null, int level = 1)
    {
        if (null == A)
            A = this.GetLevelsConfiguration(level).ResourceGeneration;

        BuildingLevelResourceGenerationConfiguration[] B = this.GetLevelsConfiguration(level + 1).ResourceGeneration;
        BuildingLevelResourceGenerationConfiguration[] C = new BuildingLevelResourceGenerationConfiguration[B.Length];

        for (int BIndex = 0; BIndex < B.Length; BIndex++)
        {
            BuildingLevelResourceGenerationConfiguration ADataThatCorrespondToBIndex =
                System.Array.Find(A, AData => AData.ResourceType == B[BIndex].ResourceType);

            float resourceGenerated = 
                null == ADataThatCorrespondToBIndex ?
                B[BIndex].ResourceGeneratedPerSeconds :
                B[BIndex].ResourceGeneratedPerSeconds - ADataThatCorrespondToBIndex.ResourceGeneratedPerSeconds;

            C[BIndex] = new BuildingLevelResourceGenerationConfiguration(B[BIndex].ResourceType, resourceGenerated);
        }

         return C;
    }

    private BuildingLevelsConfiguration GetLevelsConfiguration(int level)
    {
        return this.BuildingConfiguration.GetLevelConfigurationIfPossible(level);
    }
    //public viod
    #endregion
}
