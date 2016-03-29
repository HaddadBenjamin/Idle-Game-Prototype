using UnityEngine;
using System.Collections;
using UnityEngine.UI;

// un code bien immonde, je le reconnais.
public class BuildingButton : MonoBehaviour
{
    #region Fields
    [SerializeField]
    private string prefabName;

    private Text quantityText;
    private Text priceText;
    private EIndustryBuildingCategory industryBuildingCategory;
    private int price;
    #endregion

    #region Unity Functions
    void Start()
    {
        Transform myTransform = transform;
        BuildingConfiguration buildingConfiguration = ServiceContainer.Instance.BuildingsConfiguration.GetConfiguration(this.prefabName);
        SpriteReferencesArrays spriteManager = ServiceContainer.Instance.SpriteReferencesArrays;
        GameObject playerGameObject = ServiceContainer.Instance.GameObjectReferenceManager.Get("[PLAYER]");

        PlayerResources playerResource = playerGameObject.GetComponent<PlayerResources>();
        PlayerBuildingsManager playerBuildingCreation = playerGameObject.GetComponent<PlayerBuildingsManager>();
        ResourcePrerequisite[] resourcesPrerequisitePrice = buildingConfiguration.GetLevelConfigurationIfPossible(1).Price;
        BuildingLevelResourceGenerationConfiguration[] resourcesGenerated = buildingConfiguration.GetLevelConfigurationIfPossible(1).ResourceGeneration;

        /// Ces 3 lignes sont très déugueulasse et ultra dangereuse (non maintenable).
        int firstResourcePrice = resourcesPrerequisitePrice[0].ResourceNumber;
        EResourceCategory resourceGenerated = resourcesGenerated[0].ResourceType; // Cette ligne est dangereuse.
        EResourceCategory priceCategory = buildingConfiguration.GetLevelConfigurationIfPossible(1).Price[0].ResourceCategory; // Cette ligne est dangereuse.

        this.industryBuildingCategory = buildingConfiguration.IndustryCategory;

        this.priceText = myTransform.Find("Price Text").GetComponent<Text>();
        this.quantityText = myTransform.Find("Quantity Text").GetComponent<Text>();

        // Set de l'image et du texte du bâtiment, le nombre de bâtiment (0/3).
        myTransform.Find("Industry Image").GetComponent<Image>().sprite = spriteManager.GetResourceBinSprite(resourceGenerated);
        myTransform.Find("Industry Text").GetComponent<Text>().text = this.prefabName;
        playerGameObject.GetComponent<PlayerBuildingsManager>().
            BuildingsAnalytic.GetConstructionBuildings(industryBuildingCategory).
            CurrentValueModificationDelegate += this.UpdateBuildingQuantityText;
        UpdateBuildingQuantityText(playerGameObject.GetComponent<PlayerBuildingsManager>().BuildingsAnalytic.GetConstructionBuildings(industryBuildingCategory).CurrentValue, playerGameObject.GetComponent<PlayerBuildingsManager>().BuildingsAnalytic.GetConstructionBuildings(industryBuildingCategory).MaximumValue);
        // Set de l'image de resource généré et son texte.
        myTransform.Find("Resource Image").GetComponent<Image>().sprite = spriteManager.GetResourceSprite(resourceGenerated);
        myTransform.Find("Resource Text").GetComponent<Text>().text = resourceGenerated.ToString();

        // Set du prix, sa couleur, son image.
        this.price = firstResourcePrice;
        this.priceText.text = StringHelper.PriceToText(this.price);
        myTransform.Find("Price Image").GetComponent<Image>().sprite = spriteManager.GetResourceSprite(priceCategory);
        ServiceContainer.Instance.EventManagerResourceNumberHaveBeenUpdated.SubcribeToEvent(priceCategory, this.UpdateText);
        this.UpdateText(playerResource.GetResourceNumber(priceCategory));

        // Intéractions du bouton au clique :
        GetComponent<Button>().onClick.AddListener(() =>
        {
            //amount of buildings
            if (playerGameObject.GetComponent<PlayerBuildingsManager>().BuildingsAnalytic.GetConstructionBuildings(industryBuildingCategory).CanAdd())
            {
                if (playerResource.HaveEnoughtResource(resourcesPrerequisitePrice))
                    playerBuildingCreation.InstantiateBuilding(this.prefabName);
                else
                    ServiceContainer.Instance.TextInformationManager.AddTextInformation("You can't pay this building");
            }
            else
                ServiceContainer.Instance.TextInformationManager.AddTextInformation("You already have the number maximum of this building");
        });
    }
    #endregion

    #region Initializer
    public void Initialize(string prefabName)
    {
        this.prefabName = prefabName;
    }
    #endregion

    #region Behaviour Methods
    private void UpdateBuildingQuantityText(int current, int maximum)
    {
        this.quantityText.text = current + " / " + maximum;
    }

    private void UpdateText(int resourceNumber)
    {
        this.priceText.color = resourceNumber > this.price ?
                            ColorHelper.LightGreen :
                            ColorHelper.LightRed;
    }
    #endregion
}
