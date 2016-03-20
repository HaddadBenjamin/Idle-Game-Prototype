using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class BuildingButton : MonoBehaviour
{
    void Start()
    {
        PlayerResources playerResource = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerResources>();
        PlayerBuildings playerBuildingCreation = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerBuildings>();
        BuildingPriceAndPrefabName buildingPriceAndPrefabName = GetComponent<BuildingPriceAndPrefabName>();

        GetComponent<Button>().onClick.AddListener(() =>
        {
            if (playerResource.HaveEnoughtResource(buildingPriceAndPrefabName.ResourcesPrerequisite))
            {
                playerBuildingCreation.InstantiateBuilding(buildingPriceAndPrefabName.PrefabName, buildingPriceAndPrefabName);
            }
        });
    }
}
