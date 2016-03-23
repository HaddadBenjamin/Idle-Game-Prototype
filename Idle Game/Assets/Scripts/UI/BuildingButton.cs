using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class BuildingButton : MonoBehaviour
{
    [SerializeField]
    private string prefabName;

    void Start()
    {
        PlayerResources playerResource = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerResources>();
        PlayerBuildingsManager playerBuildingCreation = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerBuildingsManager>();
        BuildingConfiguration buildingConfiguration = ServiceLocator.Instance.BuildingsConfiguration.GetConfiguration(this.prefabName);

        GetComponent<Button>().onClick.AddListener(() =>
        {
            if (playerResource.HaveEnoughtResource(buildingConfiguration.ResourcesPrerequisite))
            {
                playerBuildingCreation.InstantiateBuilding(this.prefabName);
            }
        });
    }
}
