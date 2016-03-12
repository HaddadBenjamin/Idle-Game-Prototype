using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class OpenResourceConstructionMenu : AResourceMenu
{
    void Start()
    {
        base.BaseStart();

        base.button.onClick.AddListener(() =>
        {
            base.OpenResourceConstructionMenu();
        });
    }
}
