using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class OpenCraftEquipmentCategoryMenu : AMenuAnimationButton
{
    #region Fields
    [SerializeField]
    private EStuffCategory craftEquipmentCategoryToOpen;
    #endregion

    #region Unity Methods
    void Start ()
    {
        base.BaseStart();

        base.Button.onClick.AddListener(() =>
        {
            base.MenusAnimations.OpenCraftEquipmentCategoryMenu(craftEquipmentCategoryToOpen);
        });
    }
    #endregion
}
