using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System;

public class ResourcesDisplay : MonoBehaviour
{
    void Start()
    {
        GameObject UIPrefab = GameObject.FindGameObjectWithTag("ServiceLocator").GetComponent<ServiceLocator>().GameObjectManager.Get("Resource UI");
        SpriteManager spriteManager = GameObject.FindGameObjectWithTag("ServiceLocator").GetComponent<ServiceLocator>().SpriteManager;
        PlayerResources playerResources = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerResources>();
        Transform myTransform = transform;

        for (int resourceCategoryIndex = 0; resourceCategoryIndex < (int)EResourceCategory.Size; resourceCategoryIndex++)
        {
            GameObject UIGameObject = Instantiate(UIPrefab);
            RectTransform rectTransform = (RectTransform)UIGameObject.transform;
            ResourceDisplay resourceDisplay = UIGameObject.GetComponent<ResourceDisplay>();

            EResourceCategory resourceCategory = (EResourceCategory)resourceCategoryIndex;
            Sprite resourceImage = spriteManager.Get(resourceCategory.ToString());
            int resourceNumber = playerResources.GetResourceNumber(resourceCategory);

            UIGameObject.transform.SetParent(myTransform);
            rectTransform.SetPosition(new Vector3(0.0f, 64.0f * resourceCategoryIndex, 0.0f));

            resourceDisplay.Initialize();
            resourceDisplay.SetResourceImage(resourceImage);
            resourceDisplay.SetResourceText(resourceNumber);
        }
    }
}
