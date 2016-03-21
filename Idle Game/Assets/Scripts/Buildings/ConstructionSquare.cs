using UnityEngine;
using System.Collections;

public class ConstructionSquare : MonoBehaviour
{
    #region Fields
    /// <summary>
    /// Le material de base.
    /// </summary>
    private Material beginMaterial = null;
    /// <summary>
    /// Une référence sur le material manager, de sorte à changer le material en fonction du outline.
    /// </summary>
    private MaterialManager materialManager = null;
    /// <summary>
    /// Détermine si il y a un bâtiment à cette emplacement.
    /// </summary>
    private bool doesThereIsABuilding = false;
    /// <summary>
    /// Permet d'afficher l'effet d'outline sur cette case.
    /// </summary>
    private bool showOutline = false;
    /// <summary>
    /// Détermine où se situe cette case sur la grille de construction sur l'axe verticale.
    /// </summary>
    private int verticalPositionInGrid;
    /// <summary>
    /// Détermine où se situe cette case sur la grille de construction sur l'axe horizontal.
    /// </summary>
    private int horizontalPositionInGrid;
    #endregion

    #region Properties
    public int VerticalPositionInGrid
    {
        get { return verticalPositionInGrid; }
        private set { verticalPositionInGrid = value; }
    }

    public int HorizontalPositionInGrid
    {
        get { return horizontalPositionInGrid; }
        private set { horizontalPositionInGrid = value; }
    }

    public bool DoesThereIsABuilding
    {
        get { return doesThereIsABuilding; }
        set { doesThereIsABuilding = value; }
    }

    public bool ShowOutline
    {
        get { return showOutline; }
        set
        { 
            showOutline = value;

            if (true == showOutline)
            {
                GetComponent<Renderer>().material = this.doesThereIsABuilding ?
                                                    this.materialManager.Get("RedOutline") :
                                                    this.materialManager.Get("GreenOutline");
            }
            else
                GetComponent<Renderer>().material = this.beginMaterial;
        
        }
    }
    #endregion

    #region Unity Methods
    void Awake()
    {
        this.beginMaterial = GetComponent<Renderer>().material;
	}

    void Start()
    {
        this.materialManager =  GameObject.FindGameObjectWithTag("ServiceLocator").
                                GetComponent<ServiceLocator>().
                                MaterialManager;
    }
    #endregion 

    #region Behaviour Methods
    /// <summary>
    /// Permet de setter l'axe horizontale et verticale de cette case.
    /// </summary>
    /// <param name="verticalPositionInGrid"></param>
    /// <param name="horizontalPositionInGrid"></param>
    public void Initialize(int verticalPositionInGrid, int horizontalPositionInGrid)
    {
        this.verticalPositionInGrid = verticalPositionInGrid;
        this.horizontalPositionInGrid = horizontalPositionInGrid;
    }
    #endregion
}
