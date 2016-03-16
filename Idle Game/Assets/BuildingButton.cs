using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class BuildingButton : MonoBehaviour
{
    void Start()
    {
        PlayerResources playerResource = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerResources>();
        PlayerBuildings playerBuildingCreation = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerBuildings>();
        BuildingParameters buildingParameters = GetComponent<BuildingParameters>();

        GetComponent<Button>().onClick.AddListener(() =>
        {
            if (playerResource.HaveEnoughtResource(buildingParameters.ResourcesPrerequisite))
            {
                playerBuildingCreation.InstantiateBuilding(buildingParameters.PrefabName);
            }
        });
    }
}
