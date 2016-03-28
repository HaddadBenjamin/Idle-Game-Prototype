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

        ServiceLocator.Instance.EventManagerStuffNumberHaveBeenUpdated.SubcribeToEvent(this.stuffPrerequisite.StuffCategory, this.stuffPrerequisite.Quality, this.UpdateColorTextWithName);
    }
    #endregion

    #region Behaviour Methods
    public void UpdateColorTextWithName(int playerResource, string name)
    {
        if (this.stuffPrerequisite.Name == name)
        {
            this.text.color =
                playerResource >= this.requieredResource ?
                ColorHelper.LightGreen :
                ColorHelper.LightRed;
        }
    }
    #endregion
}
