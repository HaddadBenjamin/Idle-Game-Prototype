using UnityEngine;
using UnityEngine.UI;

public static class ResourcePrerequisiteHelper
{
    public static void SetResourcePrerequisiteUIGameObject(GameObject[] resourcePrerequisUIGameObjects, ResourcePrerequisite[] resourcePrerequisite, PlayerResources playerResources = null)
    {
        for (int resourcePrerequisiteIndex = 0;
                resourcePrerequisiteIndex < resourcePrerequisite.Length && 
                resourcePrerequisiteIndex < resourcePrerequisUIGameObjects.Length && 
                resourcePrerequisUIGameObjects[resourcePrerequisiteIndex].activeSelf;
            resourcePrerequisiteIndex++)
        {
            EResourceCategory resourcePrerequisiteCategory = resourcePrerequisite[resourcePrerequisiteIndex].ResourceCategory;
            int resourcePrerequisiteNumber = resourcePrerequisite[resourcePrerequisiteIndex].ResourceNumber;
            int playerResourceNumber = null == playerResources ? 0 : playerResources.GetResourceNumber(resourcePrerequisiteCategory);
            Text text = resourcePrerequisUIGameObjects[resourcePrerequisiteIndex].transform.Find("Resource Text").GetComponent<Text>();

            text.text =
                StringHelper.PriceToText(resourcePrerequisiteNumber) +
                (null == playerResources || resourcePrerequisiteNumber < playerResourceNumber ? "" : //Affichage prix ou prix(ce qu'il manque au joueur pour payer)
                "(" + (resourcePrerequisiteNumber - playerResourceNumber) + ")");

            resourcePrerequisUIGameObjects[resourcePrerequisiteIndex].transform.Find("Resource Image").GetComponent<Image>().sprite =
                ServiceContainer.Instance.SpriteReferencesArrays.GetResourceSprite(resourcePrerequisiteCategory);

            // Permet de mettre à jour la couleur des textes.
            UpdateCraftEquipmentResourcePrerequisiteText updateCraftResource = text.gameObject.GetComponent<UpdateCraftEquipmentResourcePrerequisiteText>();
            if (null == updateCraftResource)
                text.gameObject.AddComponent<UpdateCraftEquipmentResourcePrerequisiteText>().Initialize(resourcePrerequisiteCategory, resourcePrerequisiteNumber);
            else
            {
                updateCraftResource.UnsubscribeToEvent();
                updateCraftResource.Initialize(resourcePrerequisiteCategory, resourcePrerequisiteNumber);
            }
        }
    }
}