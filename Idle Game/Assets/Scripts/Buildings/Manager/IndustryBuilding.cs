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

    public BuildingLevelsConfiguration LevelsConfiguration { get; private set; }
    public BuildingConfiguration BuildingConfiguration { get; private set; }
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
        base.BuildingName = buildingName;
        base.BuildingLevel = 1;

        this.BuildingConfiguration = ServiceContainer.Instance.
                                    BuildingsConfiguration.GetConfiguration(base.BuildingName);

        this.LevelsConfiguration = this.GetLevelsConfiguration(this.BuildingLevel);

        this.playerResources =  ServiceContainer.Instance.
                                GameObjectReferencesArrayInScene.Get("[PLAYER]", EGameObjectReferences.Rest).
                                GetComponent<PlayerResources>();

        this.playerResources.GenerateResource(this.LevelsConfiguration.ResourceGeneration);

        //this.LevelUp();
    }


    public bool IsMaxedLevel()
    {
        return this.BuildingLevel >= ServiceContainer.Instance.BuildingsConfiguration.GetConfiguration(base.BuildingName).MaximumLevel;
    }

    public bool CanLevelup()
    {
        if (this.BuildingLevel < ServiceContainer.Instance.BuildingsConfiguration.GetConfiguration(base.BuildingName).MaximumLevel)
        {
            if (this.playerResources.HaveEnoughtResource(this.LevelsConfiguration.Price))
                return true;
            else
                ServiceContainer.Instance.TextInformationManager.AddTextInformation("You don't have enough resource to level up this building");
        }
        else
            ServiceContainer.Instance.TextInformationManager.AddTextInformation("You can't level up this building because it level is already to its maximum");
        
        return false;
    }
    
    public bool LevelUpIfPossible()
    {
        bool canLevelUp = this.CanLevelup();

        if (canLevelUp) 
            this.LevelUp();

        return canLevelUp;
    }

    private void LevelUp()
    {
        this.playerResources.GenerateResource(
            this.GetLevelsDifferenceResourceGenerationConfiguration(this.LevelsConfiguration.ResourceGeneration, this.BuildingLevel));

        ++base.BuildingLevel;

        this.LevelsConfiguration = this.GetLevelsConfiguration(this.BuildingLevel);

        this.playerResources.PayIfPossible(this.LevelsConfiguration.Price);

        //Action(level) ? pour les synergies ?
    }

    // A mettre dans un helper, DEVRA ABSOLUMENET TESTER SI LE NIVEAU DU BATIMENT PEUT LEVEL UP AVANT DETRE APPELER.
    private BuildingLevelResourceGenerationConfiguration[] GetLevelsDifferenceResourceGenerationConfiguration(BuildingLevelResourceGenerationConfiguration[] A = null, int level = 1)
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
    
    /// <summary>
    /// Appeler lors d'une suppression d'un bâtiment d'un bâtiment.
    /// </summary>
    public void UnGenerateAllResources()
    {
        int buildingLevelTemporary = 1;
        this.LevelsConfiguration = this.GetLevelsConfiguration(1);
        this.playerResources.UngenerateResource(this.GetLevelsConfiguration(buildingLevelTemporary).ResourceGeneration);

        while (buildingLevelTemporary < this.BuildingLevel)
        {
            this.playerResources.UngenerateResource(this.GetLevelsDifferenceResourceGenerationConfiguration(this.LevelsConfiguration.ResourceGeneration, buildingLevelTemporary));

            ++buildingLevelTemporary;

            this.LevelsConfiguration = this.GetLevelsConfiguration(buildingLevelTemporary);
        }
    }

    /// <summary>
    /// Appeler lors d'une suppression d'un bâtiment d'un bâtiment.
    /// </summary>
    public void GenerateteAllResources()
    {
        int buildingLevelTemporary = 1;
        this.LevelsConfiguration = this.GetLevelsConfiguration(1);
        this.playerResources.GenerateResource(this.GetLevelsConfiguration(buildingLevelTemporary).ResourceGeneration);

        ServiceContainer.Instance.TextInformationManager.AddTextInformation("building level " + this.BuildingLevel);
        while (buildingLevelTemporary < this.BuildingLevel)
        {
            this.playerResources.GenerateResource(this.GetLevelsDifferenceResourceGenerationConfiguration(this.LevelsConfiguration.ResourceGeneration, buildingLevelTemporary));

            ++buildingLevelTemporary;

            this.LevelsConfiguration = this.GetLevelsConfiguration(buildingLevelTemporary);
        }
    }

    private BuildingLevelsConfiguration GetLevelsConfiguration(int level)
    {
        return this.BuildingConfiguration.GetLevelConfigurationIfPossible(level);
    }
    //public viod
    #endregion
}
