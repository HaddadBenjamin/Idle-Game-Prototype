using UnityEngine;
using System.Collections;
using System.Collections.Generic;

/// <summary>
/// Paramètre de placement du bâtiment.
/// </summary>
public abstract class BuildingPlacement : MonoBehaviour
{
    #region Fields
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
    /// <summary>
    /// Prix de ce bâtiment, permet de spécifier une valeur sur chaque type de resource.
    /// </summary>
    [SerializeField]
    private ResourcePrerequisite[] resourcesPrerequisiteToBuildThisBuilding;
    /// <summary>
    /// Type de bâtiment : construction, décors, autre ??e
    /// </summary>
    [SerializeField]
    private EBuildingCategory buildingCategory;
    #endregion

    #region Properties
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

    public EBuildingCategory BuildingCategory
    {
        get { return buildingCategory; }
        private set { buildingCategory = value; }
    }

    public ResourcePrerequisite[] ResourcesPrerequisiteToBuildThisBuilding
    {
        get { return resourcesPrerequisiteToBuildThisBuilding; }
        private set { resourcesPrerequisiteToBuildThisBuilding = value; }
    }
    #endregion

    /// <summary>
    /// Position horizontale du bâtiment sur la grille qui ne dépasse pas sur la grille de construction.
    /// </summary>
    /// <param name="horizontal"></param>
    /// <returns></returns>
    public int GetGridHorizontalPositionWithoutOverflow(int horizontal)
    {
        if (horizontal < this.HorizontalLenght - 1)
            horizontal = this.HorizontalLenght - 1;

        return horizontal;
    }

    /// <summary>
    /// Position verticale du bâtiment sur la grille qui ne dépasse pas sur la grille de construction.
    /// </summary>
    /// <param name="vertical"></param>
    /// <returns></returns>
    public int GetGridVerticalPositionWithoutOverflow(int vertical)
    {
        if (vertical < this.VerticalLenght - 1)
            vertical = this.VerticalLenght - 1;

        return vertical;
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="horizontal"></param>
    /// <param name="vertical"></param>
    /// <returns></returns>
    public GridPosition GetGridPositionWithoutOverflow(int horizontal, int vertical)
    {
        return new GridPosition(this.GetGridHorizontalPositionWithoutOverflow(horizontal), this.GetGridVerticalPositionWithoutOverflow(vertical));
    }
}
