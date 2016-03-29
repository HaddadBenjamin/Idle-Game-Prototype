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
        GameObject UIPrefab = ServiceContainer.Instance.GameObjectReferencesArrays.Get("Resource UI", EGameObjectReferences.UI);
        SpriteReferencesArrays spriteManager = ServiceContainer.Instance.SpriteReferencesArrays;
        Transform myTransform = transform;

        this.playerResources = ServiceContainer.Instance.GameObjectReferenceManager.Get("[PLAYER]").GetComponent<PlayerResources>();
        ServiceContainer.Instance.EventManager.SubcribeToEvent(EEvent.PlayerPayResources, this.UpdateResourceNumber);

        for (int resourceCategoryIndex = 0; resourceCategoryIndex < EnumHelper.Count<EResourceCategory>(); resourceCategoryIndex++)
        {
            GameObject UIGameObject = Instantiate(UIPrefab);
            RectTransform rectTransform = (RectTransform)UIGameObject.transform;
            ResourceDisplay resourceDisplay = UIGameObject.GetComponent<ResourceDisplay>();

            EResourceCategory resourceCategory = (EResourceCategory)resourceCategoryIndex;
            Sprite resourceImage = spriteManager.GetResourceSprite(resourceCategory);
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
