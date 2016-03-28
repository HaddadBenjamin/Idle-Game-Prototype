using UnityEngine;

public class UpdateCraftEquipmentRawPrerequisiteText : AUpdateCraftEquipmentResourceText
{
    #region Initializer
    public void Initialize(ERaw rawCategory)
    {
        ServiceLocator.Instance.EventManagerRawNumberHaveBeenUpdated.SubcribeToEvent(rawCategory, base.UpdateColorText);
    }
    #endregion
}