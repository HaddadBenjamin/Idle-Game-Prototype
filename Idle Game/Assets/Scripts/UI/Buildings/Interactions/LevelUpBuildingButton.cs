using UnityEngine;
using UnityEngine.UI;

public class LevelUpBuildingButton : MonoBehaviour
{
    #region Fields
    private LevelUpButtonTooltip tooltip;
    private GameObject tooltilGameObject;

    private PlayerBuildingsManager playerBuildings;
    #endregion

    #region Unity Methods
    void Start()
    {
        this.tooltilGameObject = transform.Find("Tooltip").gameObject;
        this.tooltip = this.tooltilGameObject.GetComponent<LevelUpButtonTooltip>();

        this.playerBuildings = ServiceContainer.Instance.GameObjectReferenceManager.Get("[PLAYER]").GetComponent<PlayerBuildingsManager>();

        Button button = GetComponent<Button>();
    }

    void OnPointerEnter()
    {
        this.playerBuildings.LevelUpSelectedBuilding();
    }

    void OnPointerExit()
    {
        this.tooltilGameObject.SetActive(true);
        this.tooltip.SetContent((this.playerBuildings.GetSelectedBuilding() as IndustryBuilding).GetPriceToLevelUp());
    }
    #endregion
}