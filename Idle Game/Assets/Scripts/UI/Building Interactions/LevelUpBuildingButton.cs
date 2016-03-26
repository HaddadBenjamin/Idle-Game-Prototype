using UnityEngine;
using UnityEngine.UI;

public class LevelUpBuildingButton : MonoBehaviour
{
    #region Unity Methods
    void Start()
    {
        GetComponent<Button>().onClick.AddListener(() =>
        {
            ServiceLocator.Instance.GameObjectReferenceManager.Get("[PLAYER]").GetComponent<PlayerBuildingsManager>().LevelUpSelectedBuilding();
        });
    }
    #endregion
}