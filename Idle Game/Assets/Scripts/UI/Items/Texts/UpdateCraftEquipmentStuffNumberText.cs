using UnityEngine;
using UnityEngine.UI;

public class UpdateCraftEquipmentStuffNumberText : MonoBehaviour
{
    #region Fields
    private StuffConfiguration stuffConfiguration;
    private Text text;
    #endregion

    #region Initializer
    public void Initialize(StuffConfiguration stuffConfiguration)
    {
        this.stuffConfiguration = stuffConfiguration;

        ServiceContainer.Instance.EventManagerStuffNumberHaveBeenUpdated.SubcribeToEvent(this.stuffConfiguration.StuffCategory, EStuffQuality.Common, this.UpdateText);

        this.text = GetComponent<Text>();
    }
    #endregion

    #region Behaviour Methods
    public void UpdateText(int numberOfStuff, string name)
    {
        if (this.stuffConfiguration.StuffName == name)
            this.text.text = numberOfStuff.ToString();
    }
    #endregion
}
