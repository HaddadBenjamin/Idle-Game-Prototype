using UnityEngine;
using System.Collections;

public class BuildingsConfiguration : MonoBehaviour
{
    #region Fields
    [SerializeField]
    private BuildingConfiguration[] buildings;
    #endregion

    #region Properties
    public BuildingConfiguration GetConfiguration(string buildingName)
    {
        for (int buildingIndex = 0; buildingIndex < this.buildings.Length; buildingIndex++)
        {
            if (this.buildings[buildingIndex].PrefabName == buildingName)
                return this.buildings[buildingIndex];
        }

        return null;
    }
    #endregion
}
