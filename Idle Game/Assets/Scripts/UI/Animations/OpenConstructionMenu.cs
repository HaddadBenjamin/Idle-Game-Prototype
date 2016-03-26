using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class OpenConstructionMenu : AMenuAnimationButton
{
	void Start ()
    {
        base.BaseStart();

        base.Button.onClick.AddListener(() =>
        {
            base.MenusAnimations.OpenConstructionMenu();
        });
	}
}
