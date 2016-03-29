using UnityEngine;

public class UpdateCraftEquipmentRawPrerequisiteText : AUpdateCraftEquipmentResourceText
{
    #region Initializer
    public void Initialize(ERaw rawCategory)
    {
        ServiceContainer.Instance.EventManagerRawNumberHaveBeenUpdated.SubcribeToEvent(rawCategory, base.UpdateColorText);
    }
    #endregion
}