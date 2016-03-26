using UnityEngine;
using UnityEngine.UI;

public class MoveBuildingButton : MonoBehaviour
{
    #region Unity Methods
    void Start()
    {
        GetComponent<Button>().onClick.AddListener(() =>
        {
            ServiceLocator.Instance.GameObjectReferenceManager.Get("[PLAYER]").GetComponent<PlayerBuildingsManager>().MoveSelectedBuilding();                   
        });
    }
    #endregion
}