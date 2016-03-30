using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.Events;
using UnityEngine.UI;

public class LevelUpBuildingButton : ATooltilpHolder
{
    #region Fields
    private LevelUpButtonTooltip tooltip;
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

        this.tooltip = base.tooltilpGameObject.GetComponent<LevelUpButtonTooltip>();
        this.playerBuildings = ServiceContainer.Instance.GameObjectReferenceManager.Get("[PLAYER]").GetComponent<PlayerBuildingsManager>();

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
        this.tooltip.SetContent(industry.GetPriceToLevelUp());
    }
    #endregion
}