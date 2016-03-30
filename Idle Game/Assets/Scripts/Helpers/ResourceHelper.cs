using System;
using UnityEngine;
using UnityEngine.UI;

public static class ResourceHelper
{
    public static ResourcePrerequisite[] Add(ResourcePrerequisite[] A, ResourcePrerequisite[] B)
    {
        bool[] containResources = new bool[EnumHelper.Count<EResourceCategory>()];

        for (int i = 0; i < A.Length; i++)
            containResources[EnumHelper.GetIndex<EResourceCategory>(A[i].ResourceCategory)] = true;
        for (int i = 0; i < B.Length; i++)
            containResources[EnumHelper.GetIndex<EResourceCategory>(B[i].ResourceCategory)] = true;

        int lengthOfC = 0;
        for (int i = 0; i < containResources.Length; i++)
        {
            if (containResources[i])
                ++lengthOfC;
        }

        ResourcePrerequisite[] C = new ResourcePrerequisite[lengthOfC];
        for (int i = 0; i < A.Length; i++)
            C[i] = new ResourcePrerequisite(A[i].ResourceNumber, A[i].ResourceCategory);
         
        int cIndex = A.Length;
        for (int i = 0; i < B.Length; i++)
        {
            int findIndex = Array.FindIndex(C, c => c.ResourceCategory == B[i].ResourceCategory);

            if (-1 == findIndex)
            {
                C[cIndex] = new ResourcePrerequisite(B[i].ResourceNumber, B[i].ResourceCategory);
                ++cIndex;
            }
            else
                C[findIndex].AddResource(B[i].ResourceNumber);
        }

        return C;
    }

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