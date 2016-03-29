using UnityEngine;
using UnityEngine.UI;

public class DestroyBuildingButton : MonoBehaviour
{
    #region Unity Methods
    void Start()
    {
        GetComponent<Button>().onClick.AddListener(() =>
        {
            ServiceContainer.Instance.GameObjectReferenceManager.Get("[PLAYER]").GetComponent<PlayerBuildingsManager>().RemoveSelectedBuilding();
        });
    }
    #endregion
}