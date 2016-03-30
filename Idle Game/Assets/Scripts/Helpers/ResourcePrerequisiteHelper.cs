using UnityEngine;
using UnityEngine.UI;

public static class ResourcePrerequisiteHelper
{
    public static void SetResourcePrerequisiteUIGameObject(GameObject[] resourcePrerequisUIGameObjects, ResourcePrerequisite[] resourcePrerequisite)
    {
        for (int resourcePrerequisiteIndex = 0;
                resourcePrerequisiteIndex < resourcePrerequisite.Length && 
                resourcePrerequisiteIndex < resourcePrerequisUIGameObjects.Length && 
                resourcePrerequisUIGameObjects[resourcePrerequisiteIndex].activeSelf;
            resourcePrerequisiteIndex++)
        {
            resourcePrerequisUIGameObjects[resourcePrerequisiteIndex].transform.Find("Resource Text").GetComponent<Text>().text =
                StringHelper.PriceToText(resourcePrerequisite[resourcePrerequisiteIndex].ResourceNumber);

            resourcePrerequisUIGameObjects[resourcePrerequisiteIndex].transform.Find("Resource Image").GetComponent<Image>().sprite =
                ServiceContainer.Instance.SpriteReferencesArrays.GetResourceSprite(resourcePrerequisite[resourcePrerequisiteIndex].ResourceCategory);
        }
    }
}