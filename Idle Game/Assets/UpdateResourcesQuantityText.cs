using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class UpdateResourcesQuantityText : MonoBehaviour
{
    [SerializeField]
    private EConstructionBuildingCategory constructionBuildingCategory;
    private PlayerBuildingsAnalytic playerBuildingsAnalytic;
    private Text text;

	void Start ()
    {
        this.playerBuildingsAnalytic = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerBuildings>().BuildingsAnalytic;

        this.text = GetComponent<Text>();

        this.playerBuildingsAnalytic.GetConstructionBuildings(this.constructionBuildingCategory).CurrentValueModificationDelegate += this.UpdateQuantityText;
	}

    private void UpdateQuantityText(int current, int maximum)
    {
        this.text.text = current + " / " + maximum;
    }
}
