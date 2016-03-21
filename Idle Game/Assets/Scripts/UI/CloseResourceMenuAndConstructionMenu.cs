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
                   base.CloseResourceConstructionMenu();
                break;

                case EPlayerMenuAnimation.Construction:
                  base.CloseConstructionMenu();
                break;

                default : break;
            }

            GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerBuildingsManager>().DestroyBuildingToBuild();
        });
    }
}
