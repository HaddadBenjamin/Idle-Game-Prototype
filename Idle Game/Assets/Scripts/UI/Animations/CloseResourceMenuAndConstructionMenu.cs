using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class CloseResourceMenuAndConstructionMenu : AMenuAnimationButton
{
    void Start()
    {
        base.BaseStart();

        base.Button.onClick.AddListener(() =>
        {
            switch (base.MenusAnimations.CurrentMenuAnimation)
            {
                case EMenuAnimation.ResourceConstruction :
                    base.MenusAnimations.CloseResourceConstructionMenu();
                break;

                case EMenuAnimation.Construction:
                    base.MenusAnimations.CloseConstructionMenu();
                break;

                default : break;
            }

            ServiceLocator.Instance.EventManager.CallEvent(EEvent.DestroyBuildingToBuild);
        });
    }
}
