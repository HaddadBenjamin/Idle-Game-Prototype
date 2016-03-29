using UnityEngine;

public class UpdateCraftEquipmentResourcePrerequisiteText : AUpdateCraftEquipmentResourceText
{
    #region Initializer
    public void Initialize(EResourceCategory resourceCategory)
    {
        ServiceContainer.Instance.EventManagerResourceNumberHaveBeenUpdated.SubcribeToEvent(resourceCategory, base.UpdateColorText);
    }
    #endregion
}