using UnityEngine;
using System.Collections;

public class PlayerMenu : MonoBehaviour
{
    private EPlayerMenu currentMenu = EPlayerMenu.ConstructionMenu;

    public EPlayerMenu EPlayerMenu
    {
        get { return currentMenu; }
        set { currentMenu = value; }
    }
}
