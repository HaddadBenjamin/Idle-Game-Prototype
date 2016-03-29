using UnityEngine;

public class BuildingResourceGeneration : MonoBehaviour
{
    #region Fields
    private BuildingLevelsConfiguration levelsConfiguration;
    private int BuildingLevel;
    private string buildingName;
    #endregion

    #region Initializer
    public void Initialize(string buildingName)
    {
        this.buildingName = buildingName;

        // Récupération des données du premier niveau
        ServiceContainer.Instance.BuildingsConfiguration.GetConfiguration(this.buildingName).GetLevelConfigurationIfPossible(1);

        // Le bâtiment doit accéder au données du joueur ? c'est bien de la merde
        PlayerResources playerResource = ServiceContainer.Instance.GameObjectReferenceManager.Get("[PLAYER]").GetComponent<PlayerResources>();
    }
    #endregion

    #region Properties
    #endregion

    #region Unity Methods
    #endregion

    #region Behaviour Methods
    #endregion
}