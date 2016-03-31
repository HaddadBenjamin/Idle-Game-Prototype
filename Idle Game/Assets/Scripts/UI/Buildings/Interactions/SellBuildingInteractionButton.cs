using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class SellBuildingInteractionButton : ATooltilpHolder
{
    #region Fields
    private PlayerBuildingsManager playerBuildings;
    private SellBuildingButtonTooltip tooltip;
    #endregion

    #region Unity Methods
    void Start()
    {
        base.Initialize(
            new UICallbackData[]
            {
                new UICallbackData(EventTriggerType.PointerClick, this.ClickButtonAction),
                new UICallbackData(EventTriggerType.PointerEnter, this.EnterPointerButtonAction),
                new UICallbackData(EventTriggerType.PointerExit, this.ExitPointerButtonAction),
            });

        this.tooltip = base.tooltilpGameObject.GetComponent<SellBuildingButtonTooltip>();
        this.playerBuildings = ServiceContainer.Instance.GameObjectReferencesArrayInScene.Get("[PLAYER]", EGameObjectReferences.Rest).GetComponent<PlayerBuildingsManager>();
        this.tooltip.Initialize(GetComponent<RectTransform>()).SetActive(false);
        //this.tooltip.Initialize(GetComponent<RectTransform>()).SetActive(false);
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
        this.playerBuildings.SellSelectedBuilding();

        this.SetTooltilContent();
    }

    protected override void SetTooltilContent()
    {
        IndustryBuilding industry = (this.playerBuildings.GetSelectedBuilding() as IndustryBuilding);

        if (null == industry)
            this.tooltilpGameObject.SetActive(false);
        else
            this.tooltip.SetContent(industry.GetSellPrice(), true);
    }
    #endregion
}