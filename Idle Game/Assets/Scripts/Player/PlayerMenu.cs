using UnityEngine;
using System.Collections;

public class PlayerMenu : MonoBehaviour
{
    private EPlayerMenu currentMenu = EPlayerMenu.Default;

    public EPlayerMenu CurrentMenu
    {
        get { return currentMenu; }
        set { currentMenu = value; }
    }
}
