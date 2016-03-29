using System;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// Préparez-vous à pleurer, ce code est crade comme pas permis.
/// </summary>
public class GenerateCraftEquipmentButtons : MonoBehaviour
{
    #region Fields
    private GameObject[] craftCategoriesEquipmentButtonsContainer;
    private Transform myTransform;
    private Image borderImage;
    #endregion

    #region Constructor
    #endregion

    #region Properties
    #endregion

    #region Unity Methods
    void Awake()
    {
        this.myTransform = transform;
        this.borderImage = GetComponent<Image>();
    }

    void Start()
    {

        this.InitializeAndGenerateCraftCategoryEquipmentButtonsContainer();

        PlayerResources playerResources = ServiceContainer.Instance.GameObjectReferenceManager.Get("[PLAYER]").GetComponent<PlayerResources>();
        
        // Parcours de tous les types de catégorie d'équipement de sorte à générer chaque recettes de chaque types d'équipements.
        for (int stuffCategoryIndex = 0; stuffCategoryIndex < EnumHelper.Count<EStuffCategory>(); stuffCategoryIndex++)
        {
            Transform parentTransform = this.craftCategoriesEquipmentButtonsContainer[stuffCategoryIndex].transform;

            StuffConfiguration[] stuffsConfiguration = ServiceContainer.Instance.StuffsConfiguration.GetStuffsConfiguration((EStuffCategory)stuffCategoryIndex);

            for (int stuffConfigurationIndex = 0; stuffConfigurationIndex < stuffsConfiguration.Length; stuffConfigurationIndex++)
            {
                // Instantiation de la prefab du bouton d'équipement.
                GameObject craftEquipmentButton = ServiceContainer.Instance.GameObjectReferencesArrays.Instantiate(
                    "Craft Equipment Button",
                    new Vector3(41.0f + stuffConfigurationIndex * 276.0f, 121.0f, 0.0f),
                    Vector3.zero,
                    Vector3.one,
                    parentTransform,
                    EGameObjectReferences.UI);
                Transform craftEquipmentButtonTransform = craftEquipmentButton.transform;

                // Set de son image et de son texte.
                craftEquipmentButtonTransform.Find("Equipment Image").GetComponent<Image>().sprite = ServiceContainer.Instance.SpriteReferencesArrays.Get(
                    stuffsConfiguration[stuffConfigurationIndex].StuffName,
                    (EStuffCategory)stuffCategoryIndex);
                craftEquipmentButtonTransform.Find("Equipment Text").GetComponent<Text>().text = stuffsConfiguration[stuffConfigurationIndex].StuffName;
                craftEquipmentButtonTransform.Find("Chest Text").gameObject.AddComponent<UpdateCraftEquipmentStuffNumberText>().Initialize(stuffsConfiguration[stuffConfigurationIndex]);
           
                // Set des resources prerequites avec leur texte et images.
                int prerequisiteIndex = 0;
                var resourcesPrerequisite = stuffsConfiguration[stuffConfigurationIndex].ResourcesPrerequisite;
                var rawsPrerequisite = stuffsConfiguration[stuffConfigurationIndex].RawsPrerequisite;
                var stuffsPrerequisite = stuffsConfiguration[stuffConfigurationIndex].StuffsPrerequisite;
                for (int resourcePrerequisiteIndex = 0; resourcePrerequisiteIndex < resourcesPrerequisite.Length; resourcePrerequisiteIndex++)
                {
                    GameObject resourceUI = this.GetResourceUIGameObject(resourcePrerequisiteIndex, craftEquipmentButtonTransform);
                    Transform resourceUITransform = resourceUI.transform;
                    Text resourceText = resourceUITransform.Find("Resource Text").GetComponent<Text>();

                    resourceUITransform.Find("Resource Image").GetComponent<Image>().sprite =
                        ServiceContainer.Instance.SpriteReferencesArrays.GetResourceSprite(resourcesPrerequisite[resourcePrerequisiteIndex].ResourceCategory);
                    resourceText.text = resourcesPrerequisite[resourcePrerequisiteIndex].ResourceNumber.ToString();
                    resourceText.gameObject.AddComponent<UpdateCraftEquipmentResourcePrerequisiteText>().Initialize(resourcesPrerequisite[resourcePrerequisiteIndex].ResourceCategory);

                    ++prerequisiteIndex;
                }

                for (int rawPrerequisiteIndex = 0; rawPrerequisiteIndex < rawsPrerequisite.Length; rawPrerequisiteIndex++)
                {
                    GameObject resourceUI = this.GetResourceUIGameObject(prerequisiteIndex, craftEquipmentButtonTransform);
                    Transform resourceUITransform = resourceUI.transform;
                    Text resourceText = resourceUITransform.Find("Resource Text").GetComponent<Text>();

                    resourceUITransform.Find("Resource Image").GetComponent<Image>().sprite =
                        ServiceContainer.Instance.SpriteReferencesArrays.GetRawSprite(rawsPrerequisite[rawPrerequisiteIndex].RawCategory);
                    resourceText.text = rawsPrerequisite[rawPrerequisiteIndex].Number.ToString();
                    resourceText.gameObject.AddComponent<UpdateCraftEquipmentRawPrerequisiteText>().Initialize(rawsPrerequisite[rawPrerequisiteIndex].RawCategory);

                    ++prerequisiteIndex;
                }

                for (int stuffPrerequisiteIndex = 0; stuffPrerequisiteIndex < stuffsPrerequisite.Length; stuffPrerequisiteIndex++)
                {
                    GameObject resourceUI = this.GetResourceUIGameObject(prerequisiteIndex, craftEquipmentButtonTransform);
                    Transform resourceUITransform = resourceUI.transform;
                    Text resourceText = resourceUITransform.Find("Resource Text").GetComponent<Text>();

                    // A REFAIRE ABSOLUMENT
                    resourceUITransform.Find("Resource Image").GetComponent<Image>().sprite =
                       ServiceContainer.Instance.SpriteReferencesArrays.Get(stuffsPrerequisite[stuffPrerequisiteIndex].Name, stuffsPrerequisite[stuffPrerequisiteIndex].StuffCategory);
                    resourceText.text = stuffsPrerequisite[stuffPrerequisiteIndex].Number.ToString();
                    resourceText.gameObject.AddComponent<UpdateCraftEquipmentStuffPrerequisiteText>().Initialize(stuffsPrerequisite[stuffPrerequisiteIndex]);
                   
                    ++prerequisiteIndex;
                }

                craftEquipmentButton.AddComponent<CraftEquipmentButton>().Initialize(stuffsConfiguration[stuffConfigurationIndex]);
                // position puis image et texte, resource c'est bon, parcontre raw et item il me faudra un manager
            }
        }

        // Désactive tous les conteneur de boutons trié par catégorie.
        this.DisableCraftCategoriesEquipmentButtonsContainer();
    }
    #endregion

    #region Behaviour Methods

    private GameObject GetResourceUIGameObject(int prerequisiteIndex, Transform craftEquipmentButtonTransform, int numberOfElementsPerLine = 2)
    {
        int line = prerequisiteIndex / numberOfElementsPerLine;

        return ServiceContainer.Instance.GameObjectReferencesArrays.Instantiate(
                "Resource Equipment Prerequisite UI",
                new Vector3(prerequisiteIndex % numberOfElementsPerLine * 119.0f, line * -51.0f, 0.0f),
                Vector3.zero,
                Vector3.one,
                craftEquipmentButtonTransform,
                EGameObjectReferences.UI);
    }

    private void InitializeAndGenerateCraftCategoryEquipmentButtonsContainer()
    {
        this.craftCategoriesEquipmentButtonsContainer = new GameObject[EnumHelper.Count<EStuffCategory>()];

        for (int stuffCategoryIndex = 0; stuffCategoryIndex < EnumHelper.Count<EStuffCategory>(); stuffCategoryIndex++)
        {
            this.craftCategoriesEquipmentButtonsContainer[stuffCategoryIndex] = ServiceContainer.Instance.GameObjectReferencesArrays.Instantiate(
                "Empty Rect Transform",
                Vector3.zero,
                Vector3.zero,
                Vector3.one,
                this.myTransform,
                EGameObjectReferences.UI);
            this.craftCategoriesEquipmentButtonsContainer[stuffCategoryIndex].name = ((EStuffCategory)stuffCategoryIndex).ToString();
        }
    }

    public void DisableCraftCategoriesEquipmentButtonsContainer()
    {
        Array.ForEach(this.craftCategoriesEquipmentButtonsContainer, equipmentButtonsContainer => equipmentButtonsContainer.SetActive(false));

        this.borderImage.enabled = false;
    }

    public void EnableCraftCategoryEquipmentButtonsContainer(EStuffCategory stuffCategory)
    {
        this.DisableCraftCategoriesEquipmentButtonsContainer();

        this.craftCategoriesEquipmentButtonsContainer[EnumHelper.GetIndex<EStuffCategory>(stuffCategory)].SetActive(true);

        this.borderImage.enabled = true;
    }
    #endregion
}