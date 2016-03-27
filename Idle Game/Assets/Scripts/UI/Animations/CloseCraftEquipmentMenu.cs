using UnityEngine;

public class CloseCraftEquipmentMenu : AMenuAnimationButton
{
    void Start()
    {
        base.BaseStart();

        base.Button.onClick.AddListener(() =>
        {
            base.MenusAnimations.CloseCraftEquipmentMenu();
        });
    }
}