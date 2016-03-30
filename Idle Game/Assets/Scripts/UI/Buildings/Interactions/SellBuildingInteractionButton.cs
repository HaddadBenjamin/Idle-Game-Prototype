using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class SellBuildingInteractionButton : MonoBehaviour
{
    #region Fields
    private GameObject tooltilpGameObject;
    private Transform tooltilpTransform;

    private PlayerBuildingsManager playerBuildings;
    private bool onPointerEnter;
    #endregion

    #region Unity Methods
    void Start()
    {
        this.tooltilpGameObject = transform.Find("Tooltip").gameObject;
        this.tooltilpTransform = this.tooltilpGameObject.transform;
        this.playerBuildings = ServiceContainer.Instance.GameObjectReferenceManager.Get("[PLAYER]").GetComponent<PlayerBuildingsManager>();

        GetComponent<Button>().onClick.AddListener(() =>
        {
            this.playerBuildings.SellSelectedBuilding();
        });

        UICallbackHelper.AddCallbacksToEventTrigger(
            GetComponent<EventTrigger>(),
            new UICallbackData[]
            {
                new UICallbackData(EventTriggerType.PointerClick, this.ClickButtonAction),
                new UICallbackData(EventTriggerType.PointerEnter, this.EnterPointerButtonAction),
                new UICallbackData(EventTriggerType.PointerExit, this.ExitPointerButtonAction),
            });

        //this.tooltip.Initialize(GetComponent<RectTransform>()).SetActive(false);
    }
    #endregion

    #region Unity Methods
    void Update()
    {
        if (this.onPointerEnter)
        {
            this.tooltilpTransform.position = Input.mousePosition;
        }
    }
    #endregion

    #region Behaviour Methods
    private void ClickButtonAction(BaseEventData data)
    {
        this.playerBuildings.LevelUpSelectedBuilding();

        this.SetTooltilContent();
    }

    private void EnterPointerButtonAction(BaseEventData data)
    {
        this.tooltilpGameObject.SetActive(true);

        this.SetTooltilContent();
        
        this.onPointerEnter = true;
    }

    private void ExitPointerButtonAction(BaseEventData data)
    {
        this.tooltilpGameObject.SetActive(false);

        this.onPointerEnter = false;
    }

    private void SetTooltilContent()
    {
        //IndustryBuilding industry = (this.playerBuildings.GetSelectedBuilding() as IndustryBuilding);
        //this.tooltip.SetContent(industry.GetPriceToLevelUp());
    }
    #endregion
}