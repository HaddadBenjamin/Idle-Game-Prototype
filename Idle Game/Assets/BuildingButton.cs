using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class BuildingButton : MonoBehaviour
{
    void Start()
    {
        PlayerResources playerResource = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerResources>();
        PlayerBuildingCreation playerBuildingCreation = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerBuildingCreation>();
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
