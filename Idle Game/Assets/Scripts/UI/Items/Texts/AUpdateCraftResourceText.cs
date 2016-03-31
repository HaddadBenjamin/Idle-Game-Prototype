using UnityEngine;
using UnityEngine.UI;

public class AUpdateCraftEquipmentResourceText : MonoBehaviour
{
    #region Fields
    protected Text text;
    protected int requieredResource;
    protected bool positiveColor = false;
    #endregion

    #region Unity Methods
    void Awake()
    {
        this.text = GetComponent<Text>();
    }
    #endregion

    #region Behaviour Methods
    public void UpdateColorText(int playerResource)
    {
        this.text.color =   positiveColor ? ColorHelper.LightGreen :
                            playerResource >= this.requieredResource ? ColorHelper.LightGreen :
                                                                       ColorHelper.LightRed;
    }
    #endregion
}