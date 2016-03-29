using UnityEngine;
using UnityEngine.UI;

public class AUpdateCraftEquipmentResourceText : MonoBehaviour
{
    #region Fields
    protected Text text;
    protected int requieredResource;
    #endregion

    #region Unity Methods
    void Awake()
    {
        this.text = GetComponent<Text>();
        this.requieredResource = System.Int32.Parse(this.text.text);
    }
    #endregion

    #region Behaviour Methods
    public void UpdateColorText(int playerResource)
    {
        this.text.color =
            playerResource >= this.requieredResource ?
            ColorHelper.LightGreen :
            ColorHelper.LightRed;
    }
    #endregion
}