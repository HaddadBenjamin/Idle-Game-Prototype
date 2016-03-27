using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class OpenCraftEquipmentMenu : AMenuAnimationButton
{
	void Start ()
    {
        base.BaseStart();

        base.Button.onClick.AddListener(() =>
        {
            base.MenusAnimations.OpenCraftEquipmentMenu();
        });
	}
}
