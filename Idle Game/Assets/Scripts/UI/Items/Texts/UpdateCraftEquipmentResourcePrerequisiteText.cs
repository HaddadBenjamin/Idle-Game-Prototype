using UnityEngine;

public class UpdateCraftEquipmentRawPrerequisiteText : AUpdateCraftEquipmentResourceText
{
    #region Initializer
    public void Initialize(RawPrerequisite rawPrerequisite)
    {
        this.requieredResource = rawPrerequisite.Number;
        ServiceContainer.Instance.EventManagerRawNumberHaveBeenUpdated.SubcribeToEvent(rawPrerequisite.RawCategory, base.UpdateColorText);
    }
    #endregion
}