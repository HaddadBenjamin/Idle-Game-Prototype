using UnityEngine;

public class CloseBuildingInteractionsMenu : AMenuAnimationButton
{
    #region Unity Methods
    void Start()
    {
        base.BaseStart();

        base.Button.onClick.AddListener(() =>
        {
            base.MenusAnimations.CloseBuildingInteractionsMenu();
        });
    }
    #endregion
}