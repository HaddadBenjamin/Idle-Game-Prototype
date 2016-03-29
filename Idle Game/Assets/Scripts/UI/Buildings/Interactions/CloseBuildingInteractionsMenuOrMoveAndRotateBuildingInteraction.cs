using UnityEngine;

public class CloseBuildingInteractionsMenuOrMoveAndRotateBuildingInteraction : AMenuAnimationButton
{
    #region Unity Methods
    void Start()
    {
        base.BaseStart();

        base.Button.onClick.AddListener(() =>
        {
            PlayerBuildingsManager playerBuildingsManager = ServiceContainer.Instance.GameObjectReferenceManager.Get("[PLAYER]").GetComponent<PlayerBuildingsManager>();
            switch (ServiceContainer.Instance.GameObjectReferenceManager.Get("[PLAYER]").GetComponent<PlayerBuildingsManager>().BuildingManagerMode)
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