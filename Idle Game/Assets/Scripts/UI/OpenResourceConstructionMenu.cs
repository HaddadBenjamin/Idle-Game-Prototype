using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class OpenResourceConstructionMenu : AResourceMenu
{
    void Start()
    {
        base.button.onClick.AddListener(() =>
        {
            base.OpenResourceConstructionMenu();
        });
    }
}
