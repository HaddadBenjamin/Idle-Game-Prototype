using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class CloseResourceMenuAndConstructionMenu : AResourceMenu
{
    void Start()
    {
        base.button.onClick.AddListener(() =>
        {
            if (base.playerMenu.CurrentMenu == EPlayerMenu.Construction)
                base.OpenConstructionMenu();
        });
    }
}
