using UnityEngine;
using System.Collections;

/// <summary>
/// Conteneur de BuildingConfiguration.
/// </summary>
public class BuildingsConfiguration : MonoBehaviour
{
    #region Fields
    /// <summary>
    /// Les configurations de chaque bâtiment.
    /// </summary>
    [SerializeField]
    private BuildingConfiguration[] buildings;
    #endregion

    #region Unity Methods
    void Awake()
    {
        System.Array.ForEach(this.buildings, building => building.Initialize()); 
    }
    #endregion

    #region Behaviour Methods
    /// <summary>
    /// Récupère la configuration lié à un nom de bâtiment.
    /// </summary>
    /// <param name="buildingName"></param>
    /// <returns></returns>
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
