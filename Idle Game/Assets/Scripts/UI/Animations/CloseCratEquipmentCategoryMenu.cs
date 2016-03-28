using UnityEngine;

public class CloseCratEquipmentCategoryMenu : AMenuAnimationButton
{
    void Start()
    {
        base.BaseStart();

        base.Button.onClick.AddListener(() =>
        {
            switch (base.MenusAnimations.CurrentMenuAnimation)
            {
                case EMenuAnimation.CraftEquipmentCategory :
                    base.MenusAnimations.CloseCraftEquipmentCategoryMenu();
                break;

                case EMenuAnimation.CraftEquipment:
                    base.MenusAnimations.CloseCraftEquipmentMenu();
                break;

                default : break;
            }
        });
    }
}