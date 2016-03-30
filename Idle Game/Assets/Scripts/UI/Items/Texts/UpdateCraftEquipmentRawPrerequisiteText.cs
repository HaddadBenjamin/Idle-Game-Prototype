using UnityEngine;

public class UpdateCraftEquipmentResourcePrerequisiteText : AUpdateCraftEquipmentResourceText
{
    #region Fields
    private EResourceCategory resourceCategory;
    #endregion

    #region Initializer
    public void Initialize(EResourceCategory resourceCategory, int requieredResource)
    {
        this.resourceCategory = resourceCategory;
        this.requieredResource = requieredResource;

        this.SubscribeToEvent();
    }
    #endregion

    #region Behaviour Methods
    public void SubscribeToEvent()
    {
        ServiceContainer.Instance.EventManagerResourceNumberHaveBeenUpdated.SubcribeToEvent(resourceCategory, base.UpdateColorText);
    }

    public void UnsubscribeToEvent()
    {
        ServiceContainer.Instance.EventManagerResourceNumberHaveBeenUpdated.UnsubcribeToEvent(resourceCategory, base.UpdateColorText);
    }
    #endregion
}