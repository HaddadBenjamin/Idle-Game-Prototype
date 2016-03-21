using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class UpdateResourcesQuantityText : MonoBehaviour
{
    [SerializeField]
    private EIndustryBuildingCategory constructionBuildingCategory;
    private BuildingsAnalytic playerBuildingsAnalytic;
    private Text text;

	void Start ()
    {
        this.playerBuildingsAnalytic = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerBuildingsManager>().BuildingsAnalytic;

        this.text = GetComponent<Text>();

        this.playerBuildingsAnalytic.GetConstructionBuildings(this.constructionBuildingCategory).CurrentValueModificationDelegate += this.UpdateQuantityText;
	}

    private void UpdateQuantityText(int current, int maximum)
    {
        this.text.text = current + " / " + maximum;
    }
}
