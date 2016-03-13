using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class CloseResourceMenuAndConstructionMenu : AResourceMenu
{
    void Start()
    {
        base.BaseStart();

        base.button.onClick.AddListener(() =>
        {
            switch (base.playerMenuAnimation.CurrentMenuAnimation)
            {
                case EPlayerMenuAnimation.ResourceConstruction :
                    Debug.Log("fuck this hsit0");
                   base.CloseResourceConstructionMenu();
                break;

                case EPlayerMenuAnimation.Construction:
                  base.CloseConstructionMenu();
                break;

                default : break;
            }

            Debug.Log(base.playerMenuAnimation.CurrentMenuAnimation);
        });
    }
}
