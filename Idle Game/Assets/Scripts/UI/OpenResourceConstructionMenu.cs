using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class OpenResourceConstructionMenu : AMenuAnimationButton
{
    void Start()
    {
        base.BaseStart();

        base.Button.onClick.AddListener(() =>
        {
            base.MenusAnimations.OpenResourceConstructionMenu();
        });
    }
}
