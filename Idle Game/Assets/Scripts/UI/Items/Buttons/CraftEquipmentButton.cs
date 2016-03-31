using UnityEngine;
using UnityEngine.UI;

public class CraftEquipmentButton : MonoBehaviour
{
    #region Fields
    private PlayerStuffs playerStuffs;
    private PlayerResources playerResources;
    private PlayerRaws playerRaws;
    private StuffConfiguration stuffConfiguration;
    #endregion

    #region UnityFunctions
    void Awake()
    {
        this.playerStuffs = ServiceContainer.Instance.GameObjectReferencesArrayInScene.Get("[PLAYER]", EGameObjectReferences.Rest).GetComponent<PlayerStuffs>();
        this.playerResources = ServiceContainer.Instance.GameObjectReferencesArrayInScene.Get("[PLAYER]", EGameObjectReferences.Rest).GetComponent<PlayerResources>();
        this.playerRaws = ServiceContainer.Instance.GameObjectReferencesArrayInScene.Get("[PLAYER]", EGameObjectReferences.Rest).GetComponent<PlayerRaws>();
    }
    #endregion

    #region Initializer
    public void Initialize(StuffConfiguration stuffConfiguration)
    {
        this.stuffConfiguration = stuffConfiguration;

        GetComponent<Button>().onClick.AddListener(() =>
        {
            if (!this.playerResources.HaveEnoughtResource(this.stuffConfiguration.ResourcesPrerequisite))
                ServiceContainer.Instance.TextInformationManager.AddTextInformation("You dont have enough resources to craft this " + this.stuffConfiguration.StuffCategory, ETextInformation.Warning);
            else
            {
                if (!this.playerRaws.HaveEnoughtRaw(this.stuffConfiguration.RawsPrerequisite))
                    ServiceContainer.Instance.TextInformationManager.AddTextInformation("You dont have enough raws to craft this " + this.stuffConfiguration.StuffCategory, ETextInformation.Warning);
                else
                {
                    if (!this.playerStuffs.HaveEnoughtStuff(this.stuffConfiguration.StuffsPrerequisite))
                        ServiceContainer.Instance.TextInformationManager.AddTextInformation("You dont have enough stuffs to craft this " + this.stuffConfiguration.StuffCategory, ETextInformation.Warning);
                    else
                    {
                        this.playerResources.Pay(this.stuffConfiguration.ResourcesPrerequisite);
                        this.playerRaws.Pay(this.stuffConfiguration.RawsPrerequisite);
                        this.playerStuffs.Pay(this.stuffConfiguration.StuffsPrerequisite);

                        {
                            CraftEquipmentCaseButton equipmentCase = ServiceContainer.Instance.
                                GameObjectReferencesArrayInScene.Get("Craft Equipment Case Menu", EGameObjectReferences.UI).
                                GetComponent<GenerateCraftEquipmentCaseButtons>().GetAvailableCase();

                            if (null != equipmentCase)
                            {
                                equipmentCase.AddItemToColect(this.stuffConfiguration);

                                ServiceContainer.Instance.TextInformationManager.AddTextInformation("You start the crafting of " + stuffConfiguration.StuffName);
                            }
                            else
                                ServiceContainer.Instance.TextInformationManager.AddTextInformation("None crafting case is available", ETextInformation.Warning);
                        }
                    }
                }
            }
        });
    }
    #endregion
}