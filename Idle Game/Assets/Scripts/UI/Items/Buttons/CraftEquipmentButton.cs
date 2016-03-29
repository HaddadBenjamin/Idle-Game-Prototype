using UnityEngine;
using UnityEngine.UI;

public class CraftEquipmentButton : MonoBehaviour
{
    #region Fields
    private StuffConfiguration stuffConfiguration;
    #endregion

    #region Initializer
    public void Initialize(StuffConfiguration stuffConfiguration)
    {
        this.stuffConfiguration = stuffConfiguration;

        GetComponent<Button>().onClick.AddListener(() =>
        {
            CraftEquipmentCaseButton equipmentCase = ServiceContainer.Instance.
                GameObjectReferenceManager.Get("Craft Equipment Case Menu").
                GetComponent<GenerateCraftEquipmentCaseButtons>().GetAvailableCase();

            if (null != equipmentCase)
            {
                equipmentCase.AddItemToColect(this.stuffConfiguration);
                
                ServiceContainer.Instance.TextInformationManager.AddTextInformation("You start the crafting of " + stuffConfiguration.StuffName);
            }
            else
                ServiceContainer.Instance.TextInformationManager.AddTextInformation("None crafting case is available", ETextInformation.Warning);
        });
    }
    #endregion
}