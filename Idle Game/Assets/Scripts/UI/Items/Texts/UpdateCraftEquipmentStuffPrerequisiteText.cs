using UnityEngine;

public class UpdateCraftEquipmentStuffPrerequisiteText : AUpdateCraftEquipmentResourceText
{
    #region Fields
    private StuffPrerequisite stuffPrerequisite;
    #endregion

    #region Initializer
    public void Initialize(StuffPrerequisite stuffPrerequisite)
    {
        this.stuffPrerequisite = stuffPrerequisite;
        this.requieredResource = this.stuffPrerequisite.Number;

        ServiceContainer.Instance.EventManagerStuffNumberHaveBeenUpdated.SubcribeToEvent(this.stuffPrerequisite.StuffCategory, this.stuffPrerequisite.Quality, this.UpdateColorTextWithName);
    }
    #endregion

    #region Behaviour Methods
    public void UpdateColorTextWithName(int playerResource, string stuffName)
    {
        if (this.stuffPrerequisite.Name == stuffName)
        {
            this.text.color =
                playerResource >= this.requieredResource ?
                ColorHelper.LightGreen :
                ColorHelper.LightRed;
        }
    }
    #endregion
}
