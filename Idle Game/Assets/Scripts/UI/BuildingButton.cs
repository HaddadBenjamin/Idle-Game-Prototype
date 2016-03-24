using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class BuildingButton : MonoBehaviour
{
    [SerializeField]
    private string prefabName;

    void Start()
    {
        PlayerResources playerResource = ServiceLocator.Instance.GameObjectReferenceManager.Get("[PLAYER]").GetComponent<PlayerResources>();
        PlayerBuildingsManager playerBuildingCreation = ServiceLocator.Instance.GameObjectReferenceManager.Get("[PLAYER]").GetComponent<PlayerBuildingsManager>();
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
