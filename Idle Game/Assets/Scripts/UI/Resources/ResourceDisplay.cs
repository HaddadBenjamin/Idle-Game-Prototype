using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class ResourceDisplay : MonoBehaviour
{
    #region Fields
    private Image resourceImage;
    private Text resourceText;
    private EResourceCategory resourceCategory;
    #endregion

    #region Initializer
    public void Initialize(EResourceCategory resourceCategory)
    {
        this.resourceImage = transform.Find("Resource Image").GetComponent<Image>();
        this.resourceText = transform.Find("Resource Number").GetComponent<Text>();

        this.resourceCategory = resourceCategory;

        ServiceLocator.Instance.EventManagerResourceNumberHaveBeenUpdated.SubcribeToEvent(this.resourceCategory, this.SetResourceText);
    }
    #endregion

    #region Behaviour Methods
    public void SetResourceImage(Sprite image)
    {
        this.resourceImage.sprite = image;
    }

    public void SetResourceText(int resourceNumber)
    {
        this.resourceText.text = resourceNumber.ToString();
    }
    #endregion
}
