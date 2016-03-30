using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.Events;
using UnityEngine.UI;

public class LevelUpBuildingButton : MonoBehaviour
{
    #region Fields
    private LevelUpButtonTooltip tooltip;
    private GameObject tooltilGameObject;
    private Transform tooltilTransform;

    private PlayerBuildingsManager playerBuildings;
    private bool onPointerEnter;
    #endregion

    #region Unity Methods
    void Start()
    {
        this.tooltilGameObject = transform.Find("Tooltip").gameObject;
        this.tooltip = this.tooltilGameObject.GetComponent<LevelUpButtonTooltip>();
        this.tooltilTransform = this.tooltilGameObject.transform;

        this.playerBuildings = ServiceContainer.Instance.GameObjectReferenceManager.Get("[PLAYER]").GetComponent<PlayerBuildingsManager>();

        UICallbackHelper.AddCallbacksToEventTrigger(
            GetComponent<EventTrigger>(),
            new UICallbackData[]
            {
                new UICallbackData(EventTriggerType.PointerClick, this.ClickButtonAction),
                new UICallbackData(EventTriggerType.PointerEnter, this.EnterPointerButtonAction),
                new UICallbackData(EventTriggerType.PointerExit, this.ExitPointerButtonAction),
            });

        this.tooltip.Initialize(GetComponent<RectTransform>()).SetActive(false);
    }
    #endregion

    #region Unity Methods
    void Update()
    {
        if (this.onPointerEnter)
        {
            this.tooltilTransform.position = Input.mousePosition;
        }
    }
    #endregion

    #region Behaviour Methods
    private void ClickButtonAction(BaseEventData data)
    {
        this.playerBuildings.LevelUpSelectedBuilding();
    }

    private void EnterPointerButtonAction(BaseEventData data)
    {
        this.tooltilGameObject.SetActive(true);
        this.tooltip.SetContent((this.playerBuildings.GetSelectedBuilding() as IndustryBuilding).GetPriceToLevelUp());
        
        this.onPointerEnter = true;
    }

    private void ExitPointerButtonAction(BaseEventData data)
    {
        this.tooltilGameObject.SetActive(false);

        this.onPointerEnter = false;
    }
    #endregion
}