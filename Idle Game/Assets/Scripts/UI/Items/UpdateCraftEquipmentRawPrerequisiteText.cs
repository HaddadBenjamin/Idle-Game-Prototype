using UnityEngine;

public class UpdateCraftEquipmentResourcePrerequisiteText : AUpdateCraftEquipmentResourceText
{
    #region Initializer
    public void Initialize(EResourceCategory resourceCategory)
    {
        ServiceLocator.Instance.EventManagerResourceNumberHaveBeenUpdated.SubcribeToEvent(resourceCategory, base.UpdateColorText);
    }
    #endregion
}