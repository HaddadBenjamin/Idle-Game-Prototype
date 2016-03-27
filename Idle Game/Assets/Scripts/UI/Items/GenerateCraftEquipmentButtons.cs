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
        StuffConfiguration[] stuffsConfiguration = ServiceLocator.Instance.StuffsConfiguration.GetStuffsConfiguration(this.stuffCategory);
        Transform myTransform = transform;

        for (int stuffConfigurationIndex = 0; stuffConfigurationIndex < stuffsConfiguration.Length; stuffConfigurationIndex++)
        {
            GameObject craftEquipmentButton = ServiceLocator.Instance.GameObjectManager.Instantiate(
                "Craft Equipment Button",
                new Vector3(41.0f + stuffConfigurationIndex * 276.0f, 121.0f, 0.0f),
                Vector3.zero,
                Vector3.one,
                myTransform);

            //Transform craftEquipmentButtonTransform = craftEquipmentButton.transform
            //craftEquipmentButtonTransform.Find("Equipment Image").GetComponent<Image>().
        }
    }
    #endregion

    #region Behaviour Methods
    #endregion
}