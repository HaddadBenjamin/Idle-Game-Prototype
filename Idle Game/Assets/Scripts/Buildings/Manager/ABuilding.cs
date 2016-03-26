using UnityEngine;
using System.Collections;
using System.Collections.Generic;

/// <summary>
/// Paramètre de placement du bâtiment.
/// </summary>
public abstract class ABuilding : MonoBehaviour
{
    #region Fields
    /// <summary>
    /// Type de bâtiment : construction, décors, autre ??e
    /// </summary>
    [SerializeField]
    private EBuildingCategory buildingCategory;
    #endregion

    #region Properties
    public EBuildingCategory BuildingCategory
    {
        get { return buildingCategory; }
        private set { buildingCategory = value; }
    }
    #endregion


}
