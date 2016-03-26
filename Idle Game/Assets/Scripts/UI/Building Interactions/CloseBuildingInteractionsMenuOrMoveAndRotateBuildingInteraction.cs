using UnityEngine;

public class CloseBuildingInteractionsMenuOrMoveAndRotateBuildingInteraction : AMenuAnimationButton
{
    #region Unity Methods
    void Start()
    {
        base.BaseStart();

        Debug.Log(gameObject);

        base.Button.onClick.AddListener(() =>
        {
            PlayerBuildingsManager playerBuildingsManager = ServiceLocator.Instance.GameObjectReferenceManager.Get("[PLAYER]").GetComponent<PlayerBuildingsManager>();
            switch (ServiceLocator.Instance.GameObjectReferenceManager.Get("[PLAYER]").GetComponent<PlayerBuildingsManager>().BuildingManagerMode)
            {
                case EBuildingManagerMode.Move:
                    playerBuildingsManager.CancelBuildingMove();
                    break;

                default:
                    base.MenusAnimations.CloseBuildingInteractionsMenu();
                    break;
            }
        });
    }
    #endregion
}