using UnityEngine;
using System.Collections;

using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class UpdatePiecesOfFurnitureText : MonoBehaviour
{
    [SerializeField]
    private PlayerBuildingsAnalytic playerBuildingsAnalytic;
    private Text text;

    void Start()
    {
        this.playerBuildingsAnalytic = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerBuildings>().BuildingsAnalytic;

        this.text = GetComponent<Text>();

        this.playerBuildingsAnalytic.PiecesOfFurniture.CurrentValueModificationDelegate += this.UpdateQuantityText;
    }

    private void UpdateQuantityText(int current, int maximum)
    {
        this.text.text = "Capacity " + current + " / " + maximum;
    }
}

