using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System.Globalization;

public class GoldTextSubscriber : MonoBehaviour
{
    private Text text;
    private int goldPrice;
    private PlayerResources playerResources;

    void Start()
    {
        this.text = GetComponent<Text>();

        this.goldPrice = transform.parent.GetComponent<BuildingPriceAndPrefabName>().ResourcesPrerequisite[0].ResourceNumber;
        this.text.text = this.goldPrice.ToString("N", new CultureInfo("en-US"));
        this.text.text = this.text.text.Remove(this.text.text.IndexOf('.'));
        
        this.playerResources = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerResources>();

        this.playerResources.SubscribeToResourcesModificationDelegate(EResourceCategory.Gold, this.UpdateText);
    }

    private void UpdateText()
    {
        int goldNumber = this.playerResources.GetResourceNumber(EResourceCategory.Gold);

        this.text.color =   goldNumber > this.goldPrice ?
                            ColorHelper.LightGreen :
                            ColorHelper.LightRed;
    }
}
