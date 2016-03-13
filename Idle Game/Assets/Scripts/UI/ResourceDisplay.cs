using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class ResourceDisplay : MonoBehaviour
{
    private Image resourceImage;
    private Text resourceText;

    public void Initialize()
    {
        this.resourceImage = transform.Find("Resource Image").GetComponent<Image>();
        this.resourceText = transform.Find("Resource Number").GetComponent<Text>();
    }

    public void SetResourceImage(Sprite image)
    {
        this.resourceImage.sprite = image;
    }

    public void SetResourceText(int resourceNumber)
    {
        this.resourceText.text = resourceNumber.ToString();
    }
}
