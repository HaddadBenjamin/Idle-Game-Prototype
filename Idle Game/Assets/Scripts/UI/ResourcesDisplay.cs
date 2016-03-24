using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System;

public class ResourcesDisplay : MonoBehaviour
{
    private ResourceDisplay[] resources;
    private PlayerResources playerResources;

    void Awake()
    {
        this.resources = new ResourceDisplay[EnumHelper.Count<EResourceCategory>()];
    }

    void Start()
    {
        GameObject UIPrefab = ServiceLocator.Instance.GameObjectManager.Get("Resource UI");
        SpriteManager spriteManager = ServiceLocator.Instance.SpriteManager;
        Transform myTransform = transform;

        this.playerResources = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerResources>();
        ServiceLocator.Instance.EventManager.SubcribeToEvent(EEvent.PlayerPayResources, this.UpdateResourceNumber);

        for (int resourceCategoryIndex = 0; resourceCategoryIndex < EnumHelper.Count<EResourceCategory>(); resourceCategoryIndex++)
        {
            GameObject UIGameObject = Instantiate(UIPrefab);
            RectTransform rectTransform = (RectTransform)UIGameObject.transform;
            ResourceDisplay resourceDisplay = UIGameObject.GetComponent<ResourceDisplay>();

            EResourceCategory resourceCategory = (EResourceCategory)resourceCategoryIndex;
            Sprite resourceImage = spriteManager.Get(resourceCategory.ToString());
            int resourceNumber = playerResources.GetResourceNumber(resourceCategory);

            this.resources[resourceCategoryIndex] = resourceDisplay;

            UIGameObject.transform.SetParent(myTransform);
            rectTransform.SetPosition(new Vector3(0.0f, 64.0f * resourceCategoryIndex, 0.0f));

            resourceDisplay.Initialize(resourceCategory);
            resourceDisplay.SetResourceImage(resourceImage);
            resourceDisplay.SetResourceText(resourceNumber);
        }
    }

    private void UpdateResourceNumber()
    {
        for (int resourceCategoryIndex = 0; resourceCategoryIndex < EnumHelper.Count<EResourceCategory>(); resourceCategoryIndex++)
        {
            EResourceCategory resourceCategory = (EResourceCategory)resourceCategoryIndex;
            int resourceNumber = playerResources.GetResourceNumber(resourceCategory);

            this.resources[resourceCategoryIndex].SetResourceText(resourceNumber);
        }
    }
}
