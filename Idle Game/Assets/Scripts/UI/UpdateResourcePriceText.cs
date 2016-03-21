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
    private PlayerResources playerResources;
    #endregion

    #region Unity Functions
    void Start()
    {
        this.text = GetComponent<Text>();

        this.price = GameObject.FindGameObjectWithTag("ServiceLocator").
            GetComponent<ServiceLocator>().
            BuildingsConfiguration.
            GetConfiguration(this.prefabName).
            GetResourcePrice(this.resourceCategory);

        this.text.text = StringHelper.PriceToText(this.price);
        
        this.playerResources = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerResources>();

        this.playerResources.SubscribeToResourcesModificationDelegate(EResourceCategory.Gold, this.UpdateText);
    }
    #endregion

    #region Behaviour Methods
    private void UpdateText()
    {
        int goldNumber = this.playerResources.GetResourceNumber(EResourceCategory.Gold);

        this.text.color =   goldNumber > this.price ?
                            ColorHelper.LightGreen :
                            ColorHelper.LightRed;
    }
    #endregion
}
