﻿using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class OpenConstructionMenu : AResourceMenu
{
	void Start ()
    {
        base.BaseStart();

        this.button.onClick.AddListener(() =>
        {
            base.OpenConstructionMenu();
        });
	}
}