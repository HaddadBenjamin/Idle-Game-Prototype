using UnityEngine;
using UnityEngine.UI;

public class GenerateCraftEquipmentButtons : MonoBehaviour
{
    #region Fields
    [SerializeField]
    private EStuffCategory stuffCategory;
    #endregion

    #region Constructor
    #endregion

    #region Properties
    #endregion

    #region Unity Methods
    void Start()
    {
        PlayerResources playerResources = ServiceLocator.Instance.GameObjectReferenceManager.Get("[PLAYER]").GetComponent<PlayerResources>();
        StuffConfiguration[] stuffsConfiguration = ServiceLocator.Instance.StuffsConfiguration.GetStuffsConfiguration(this.stuffCategory);
        Transform myTransform = transform;

        for (int stuffConfigurationIndex = 0; stuffConfigurationIndex < stuffsConfiguration.Length; stuffConfigurationIndex++)
        {
            // Instantiation de la prefab du bouton d'équipement.
            GameObject craftEquipmentButton = ServiceLocator.Instance.GameObjectManager.Instantiate(
                "Craft Equipment Button",
                new Vector3(41.0f + stuffConfigurationIndex * 276.0f, 121.0f, 0.0f),
                Vector3.zero,
                Vector3.one,
                myTransform);
            Transform craftEquipmentButtonTransform = craftEquipmentButton.transform;

            // Set de son image et de son texte.
            craftEquipmentButtonTransform.Find("Equipment Image").GetComponent<Image>().sprite = ServiceLocator.Instance.SpriteManagerReferencesArrays.Get(
                stuffsConfiguration[stuffConfigurationIndex].StuffName,
                stuffCategory);
            craftEquipmentButtonTransform.Find("Equipment Text").GetComponent<Text>().text = stuffsConfiguration[stuffConfigurationIndex].StuffName;
           
            // Set des resources prerequites avec leur texte et images.
            int prerequisiteIndex = 0;
            var resourcesPrerequisite = stuffsConfiguration[stuffConfigurationIndex].ResourcesPrerequisite;
            var rawsPrerequisite = stuffsConfiguration[stuffConfigurationIndex].RawsPrerequisite;
            var stuffsPrerequisite = stuffsConfiguration[stuffConfigurationIndex].StuffsPrerequisite;
            for (int resourcePrerequisiteIndex = 0; resourcePrerequisiteIndex < resourcesPrerequisite.Length; resourcePrerequisiteIndex++)
            {
                GameObject resourceUI = this.GetResourceUIGameObject(resourcePrerequisiteIndex, craftEquipmentButtonTransform);
                Transform resourceUITransform = resourceUI.transform;
                
                resourceUITransform.Find("Resource Image").GetComponent<Image>().sprite =
                    ServiceLocator.Instance.SpriteManagerReferencesArrays.GetResourceSprite(resourcesPrerequisite[resourcePrerequisiteIndex].ResourceCategory);
                resourceUITransform.Find("Resource Text").GetComponent<Text>().text = resourcesPrerequisite[resourcePrerequisiteIndex].ResourceNumber.ToString();

                ++prerequisiteIndex;
            }

            for (int rawPrerequisiteIndex = 0; rawPrerequisiteIndex < rawsPrerequisite.Length; rawPrerequisiteIndex++)
            {
                GameObject resourceUI = this.GetResourceUIGameObject(prerequisiteIndex, craftEquipmentButtonTransform);
                Transform resourceUITransform = resourceUI.transform;

                resourceUITransform.Find("Resource Image").GetComponent<Image>().sprite =
                    ServiceLocator.Instance.SpriteManagerReferencesArrays.GetRawSprite(rawsPrerequisite[rawPrerequisiteIndex].RawCategory);
                resourceUITransform.Find("Resource Text").GetComponent<Text>().text = rawsPrerequisite[rawPrerequisiteIndex].Number.ToString();

                ++prerequisiteIndex;
            }

            for (int stuffPrerequisiteIndex = 0; stuffPrerequisiteIndex < stuffsPrerequisite.Length; stuffPrerequisiteIndex++)
            {
                GameObject resourceUI = this.GetResourceUIGameObject(prerequisiteIndex, craftEquipmentButtonTransform);
                Transform resourceUITransform = resourceUI.transform;

                resourceUITransform.Find("Resource Image").GetComponent<Image>().sprite =
                   ServiceLocator.Instance.SpriteManagerReferencesArrays.Get(stuffsPrerequisite[stuffPrerequisiteIndex].Name, stuffsPrerequisite[stuffPrerequisiteIndex].StuffCategory);
                resourceUITransform.Find("Resource Text").GetComponent<Text>().text = stuffsPrerequisite[stuffPrerequisiteIndex].Number.ToString();

                ++prerequisiteIndex;
            }
            // position puis image et texte, resource c'est bon, parcontre raw et item il me faudra un manager
        }
    }
    #endregion

    #region Behaviour Methods

    private GameObject GetResourceUIGameObject(int prerequisiteIndex, Transform craftEquipmentButtonTransform, int numberOfElementsPerLine = 2)
    {
        int line = prerequisiteIndex / numberOfElementsPerLine;

        return ServiceLocator.Instance.GameObjectManager.Instantiate(
                "Resource Equipment Prerequisite UI",
                new Vector3(prerequisiteIndex % numberOfElementsPerLine * 119.0f, line * -51.0f, 0.0f),
                Vector3.zero,
                Vector3.one,
                craftEquipmentButtonTransform);
    }
    #endregion
}