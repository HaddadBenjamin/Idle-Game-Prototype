using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System.Globalization;

public class UpdateResourcePriceText : MonoBehaviour
{
    #region Fields
    [SerializeField]
    private EResourceCategory resourceCategory = EResourceCategory.Gold;
    [SerializeField]
    private string prefabName = "Stone Mine";

    private Text text;
    private int price;
    #endregion

    #region Unity Functions
    void Start()
    {
        this.text = GetComponent<Text>();

        this.price = ServiceLocator.Instance.
            BuildingsConfiguration.
            GetConfiguration(this.prefabName).
            GetResourcePrice(this.resourceCategory);

        this.text.text = StringHelper.PriceToText(this.price);
        
        ServiceLocator.Instance.EventManagerResourceNumberHaveBeenUpdated.SubcribeToEvent(this.resourceCategory, this.UpdateText);
    }
    #endregion

    #region Behaviour Methods
    private void UpdateText(int resourceNumber)
    {
        this.text.color =   resourceNumber > this.price ?
                            ColorHelper.LightGreen :
                            ColorHelper.LightRed;
    }
    #endregion
}
