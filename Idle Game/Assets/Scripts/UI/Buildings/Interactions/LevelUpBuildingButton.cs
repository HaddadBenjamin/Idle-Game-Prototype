using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.Events;
using UnityEngine.UI;

public class LevelUpBuildingButton : ATooltilpHolder
{
    #region Fields
    private LevelUpBuildingButtonTooltip tooltip;
    private PlayerBuildingsManager playerBuildings;
    #endregion

    #region Unity Methods
    void Start()
    {
        base.Initialize(
            new UICallbackData[]
            {
                new UICallbackData(EventTriggerType.PointerClick, this.ClickButtonAction),
                new UICallbackData(EventTriggerType.PointerEnter, base.EnterPointerButtonAction),
                new UICallbackData(EventTriggerType.PointerExit, base.ExitPointerButtonAction),
            });

        this.tooltip = base.tooltilpGameObject.GetComponent<LevelUpBuildingButtonTooltip>();
        this.playerBuildings = ServiceContainer.Instance.GameObjectReferencesArrayInScene.Get("[PLAYER]", EGameObjectReferences.Rest).GetComponent<PlayerBuildingsManager>();

        this.tooltip.Initialize(GetComponent<RectTransform>()).SetActive(false);
    }
    #endregion

    #region Unity Methods
    void Update()
    {
        base.UpdateTooltipPosition();
    }
    #endregion

    #region Behaviour Methods
    private void ClickButtonAction(BaseEventData data)
    {
        this.playerBuildings.LevelUpSelectedBuilding();

        this.SetTooltilContent();
    }

    protected override void SetTooltilContent()
    {
        IndustryBuilding industry = (this.playerBuildings.GetSelectedBuilding() as IndustryBuilding);

        if (industry == null)
            base.tooltilpGameObject.SetActive(false);
        else
        {
            ResourcePrerequisite[] priceToLevelUp = industry.GetPriceToLevelUp();

            this.tooltip.SetContent(null != priceToLevelUp ? priceToLevelUp : new ResourcePrerequisite[0]);
        }
    }
    #endregion
}