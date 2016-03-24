using UnityEngine;
using System.Collections;

/// <summary>
/// Cette classe contiend essentiellement ce qui doit être unique à chaque type de bâtiment tel que le nom de la préfab d'ûn bâtiment ou bien son prix.
/// </summary>
[System.Serializable]
public class BuildingConfiguration
{
    #region Fields
    /// <summary>
    /// Prix du bâtiment, on peut spécifier plusieurs type de resources.
    /// </summary>
    [SerializeField]
    private ResourcePrerequisite[] resourcesPrerequisite;
    /// <summary>
    /// Nom de la préfab du bâtiment.
    /// </summary>
    [SerializeField]
    private string prefabName;
    /// <summary>
    /// Type d'industrie de la préfab. 
    /// </summary>
    [SerializeField]
    private EIndustryBuildingCategory industryCategory;
    /// <summary>
    /// Nombre de cases horizontales que prend le bâtiment.
    /// </summary>
    [SerializeField]
    private byte horizontalLength;
    /// <summary>
    /// Nombre de cases verticales que prend le bâtiment.
    /// </summary>
    [SerializeField]
    private byte verticalLength;
    /// <summary>
    /// Permet de repositionner sur l'axe horizontale ce bâtiment, la valeur 1 signifie que l'on le déplace d'une case vers la droite.
    /// </summary>
    [SerializeField]
    private float horizontalOffsetNormalized;
    /// <summary>
    /// Permet de repositionner sur l'axe verticale ce bâtiment, la valeur 1 signifie que l'on le déplace d'une case vers le bas.
    /// </summary>
    [SerializeField]
    private float verticalOffsetNormalized;
    [SerializeField]
    private BuildingLevelsConfiguration[] levelsConfiguration;
    private int maximumLevel;
    #endregion

    #region Constructor
    public void Initialize()
    {
        this.maximumLevel = this.levelsConfiguration.Length;
    }
    #endregion

    #region Properties
    public int MaximumLevel
    {
        get { return maximumLevel; }
        private set { maximumLevel = value; }
    }

    public ResourcePrerequisite[] ResourcesPrerequisite
    {
        get { return resourcesPrerequisite; }
        private set { resourcesPrerequisite = value; }
    }

    public string PrefabName
    {
        get { return prefabName; }
        private set { prefabName = value; }
    }

    public EIndustryBuildingCategory IndustryCategory
    {
        get { return industryCategory; }
        private set { industryCategory = value; }
    }

    public byte HorizontalLenght
    {
        get { return horizontalLength; }
        private set { horizontalLength = value; }
    }

    public byte VerticalLenght
    {
        get { return verticalLength; }
        private set { verticalLength = value; }
    }

    public float HorizontalOffsetNormalized
    {
        get { return horizontalOffsetNormalized; }
        private set { horizontalOffsetNormalized = value; }
    }

    public float VerticalOffsetNormalized
    {
        get { return verticalOffsetNormalized; }
        private set { verticalOffsetNormalized = value; }
    }
    #endregion

    #region Behaviour
    public bool CanGetLevelConfiguration(int level)
    {
        return level <= this.levelsConfiguration.Length;
    }

    public BuildingLevelsConfiguration GetLevelConfigurationIfPossible(int level)
    {
        return  this.CanGetLevelConfiguration(level) ?
                this.GetLevelConfiguration(level) :
                null;
    }

    public BuildingLevelsConfiguration GetLevelConfiguration(int level)
    {
        return this.levelsConfiguration[level - 1];
    }
    /// <summary>
    /// Obtneir le prix d'une resource en fonction de son type de resource.
    /// </summary>
    /// <param name="resourceCategory"></param>
    /// <returns></returns>
    public int GetResourcePrice(EResourceCategory resourceCategory)
    {
        for (int resourceIndex = 0; resourceIndex < this.resourcesPrerequisite.Length; resourceIndex++)
        {
            if (this.resourcesPrerequisite[resourceIndex].ResourceCategory == resourceCategory)
                return this.resourcesPrerequisite[resourceIndex].ResourceNumber;
        }

        return 0;
    }

    /// <summary>
    /// Position horizontale du bâtiment sur la grille qui ne dépasse pas sur la grille de construction.
    /// </summary>
    /// <param name="horizontal"></param>
    /// <returns></returns>
    public int GetGridHorizontalPositionWithoutOverflow(int horizontal)
    {
        return horizontal < this.HorizontalLenght - 1 ?
                this.HorizontalLenght - 1 :
                horizontal;
    }

    /// <summary>
    /// Position verticale du bâtiment sur la grille qui ne dépasse pas sur la grille de construction.
    /// </summary>
    /// <param name="vertical"></param>
    /// <returns></returns>
    public int GetGridVerticalPositionWithoutOverflow(int vertical)
    {
        return vertical < this.VerticalLenght - 1 ?
                this.VerticalLenght - 1 :
                vertical;
    }

    /// <summary>
    /// Position verticale et horizontale sur la grille de sorte qu'elle ne dépasse pas sur la grille de construction.
    /// </summary>
    /// <param name="horizontal"></param>
    /// <param name="vertical"></param>
    /// <returns></returns>
    public GridPosition GetGridPositionWithoutOverflow(int horizontal, int vertical)
    {
        return new GridPosition(this.GetGridHorizontalPositionWithoutOverflow(horizontal), this.GetGridVerticalPositionWithoutOverflow(vertical));
    }
    #endregion
}