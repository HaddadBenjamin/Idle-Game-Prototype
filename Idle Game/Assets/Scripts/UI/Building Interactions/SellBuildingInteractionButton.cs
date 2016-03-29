using UnityEngine;
using UnityEngine.UI;

public class SellBuildingInteractionButton : MonoBehaviour
{
    #region Unity Methods
    void Start()
    {
        GetComponent<Button>().onClick.AddListener(() =>
        {
            ServiceContainer.Instance.GameObjectReferenceManager.Get("[PLAYER]").GetComponent<PlayerBuildingsManager>().SellSelectedBuilding();
        });
    }
    #endregion
}