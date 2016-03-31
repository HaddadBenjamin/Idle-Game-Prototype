using UnityEngine;

public class CloseBuildingInteractionsMenuOrMoveAndRotateBuildingInteraction : AMenuAnimationButton
{
    #region Unity Methods
    void Start()
    {
        base.BaseStart();

        base.Button.onClick.AddListener(() =>
        {
            PlayerBuildingsManager playerBuildingsManager = ServiceContainer.Instance.GameObjectReferencesArrayInScene.Get("[PLAYER]", EGameObjectReferences.Rest).GetComponent<PlayerBuildingsManager>();
            switch (ServiceContainer.Instance.GameObjectReferencesArrayInScene.Get("[PLAYER]", EGameObjectReferences.Rest).GetComponent<PlayerBuildingsManager>().BuildingManagerMode)
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