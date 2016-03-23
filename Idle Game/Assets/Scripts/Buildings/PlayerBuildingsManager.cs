using UnityEngine;
using System.Collections;

public class PlayerBuildingsManager : ABuildingManager
{
    #region Fields
    /// <summary>
    /// Lorsque l'on pose le bâtiment on doit déterminer si le joueur peut le payer et si cest le cas, alors le payer.
    /// </summary>
    private PlayerResources playerResources;

    /// <summary>
    /// Donne accès à pleins d'information spécifique à chaque bâtiment tels que sa position, son prix, son nom etc...
    /// </summary>
    private BuildingConfiguration buildingConfiguration;
    /// <summary>
    /// Permet de déterminer où l'on souhaite placer un bâtiment.
    /// </summary>
    private ConstructionSquare constructionSquare;
    /// <summary>
    /// Correspond au bâtiment que l'on souhaite construire.
    /// </summary>
    private GameObject buildingToCreateGameObject;
    /// <summary>
    /// Le nom de la prefab du bâtiment à instancier / détruire (utilisation de l'object pool).
    /// </summary>
    private string buildingName;

    /// <summary>
    /// Nous permet de placer le bâtiment sur les cases.
    /// </summary>
    private Ray ray;
    private RaycastHit hit;

    /// <summary>
    /// Si notre rayon ne trouve pas de cases cette condition sinon elle est fausse.
    /// </summary>
    private bool canPlaceTheBuildingOnGrid;

    private BuildingsAnalytic buildingsAnalytic;
    #endregion

    #region Properties
    public BuildingsAnalytic BuildingsAnalytic
    {
        get { return buildingsAnalytic; }
        private set { buildingsAnalytic = value; }
    }
    #endregion

    #region Unity Methods
    void Awake()
    {
        this.MyAwake();
        this.buildingsAnalytic = new BuildingsAnalytic();
    }

    void Start()
    {
        this.playerResources = GetComponent<PlayerResources>();

        this.buildingsAnalytic.AtStartUpdateAllMembersSubscribeToDelegateAfterInitialization();
    }

    void Update()
    {
        if (null != this.buildingToCreateGameObject)
        {
            this.PlaceBuildingAndAffectOutlineOfConstructionSquares();
            
            if (Input.GetMouseButtonDown(0))
                this.AddBuilding();
        }
    }
    #endregion

    #region Behaviour
    /// <summary>
    /// Détermine si il est possible de placer un bâtiment, il y a environ 10 conditions pour que cela soit possible dans mon gameplay.
    /// </summary>
    /// <returns></returns>
    private bool CanAddBuilding()
    {
        if (this.canPlaceTheBuildingOnGrid)
        {
            if (base.DoesItIsPossibleToBuildABuildingOnThisArea(this.constructionSquare, this.buildingConfiguration))
            {
                var minimumMaximumBuilding = this.BuildingsAnalytic.GetConstructionBuildings(this.buildingConfiguration.IndustryCategory);

                if (minimumMaximumBuilding.CanAdd())
                {
                    var piecesOfFurniture = this.buildingsAnalytic.PiecesOfFurniture;

                    if (piecesOfFurniture.CanAdd())
                    {
                        if (this.playerResources.HaveEnoughtResource(this.buildingConfiguration.ResourcesPrerequisite))
                        {
                            Debug.Log("A new building have been created");

                            return true;
                        }
                        else
                            Debug.Log("cant pay the building");
                    }
                    else
                        Debug.Log("You already build " + piecesOfFurniture.CurrentValue + " / " + piecesOfFurniture.MaximumValue + " of pieces of furniture");
                }
                else
                    Debug.Log("You already build " + minimumMaximumBuilding.CurrentValue + " / " + minimumMaximumBuilding.MaximumValue + " of this type of building");
            }
            else
                Debug.Log("There is already a building here");
        }
        else
            Debug.Log("Cant Add Building on the grid because you are not over it ");


        return false;
    }

    /// <summary>
    /// Ajoute le bâtiment si cela est possible.
    /// </summary>
    /// <returns></returns>
    private bool AddBuilding()
    {
        bool canAddBuilding = this.CanAddBuilding();

        if (canAddBuilding)
        {
            this.buildingToCreateGameObject = null;

            base.DisableAllConstructionSquaresOutline();
            base.PutThisAreaAsUnconstructible(this.constructionSquare, this.buildingConfiguration);

            this.buildingsAnalytic.GetConstructionBuildings(this.buildingConfiguration.IndustryCategory).Add();
            this.buildingsAnalytic.PiecesOfFurniture.Add();

            this.playerResources.Pay(this.buildingConfiguration.ResourcesPrerequisite);
        }

        return canAddBuilding;
    }

    /// <summary>
    /// Permet de positionner un bâtiment sur la grille de cases de constructions et active / désactive le outline sur ces cases.
    /// </summary>
    private void PlaceBuildingAndAffectOutlineOfConstructionSquares()
    {
        this.ray = Camera.main.ScreenPointToRay(Input.mousePosition);

        if (Physics.Raycast(this.ray, out this.hit, LayerMask.GetMask("ConstructionSquare")))
        {
            Transform colliderTransform = this.hit.collider.transform;

            this.constructionSquare = colliderTransform.GetComponent<ConstructionSquare>();
            this.canPlaceTheBuildingOnGrid = true;

            GridPosition buildingGridPosition = this.buildingConfiguration.GetGridPositionWithoutOverflow(constructionSquare.HorizontalPositionInGrid, constructionSquare.VerticalPositionInGrid);

            this.buildingToCreateGameObject.transform.position = base.GetNewBuildingPosition(buildingGridPosition.HorizontalGridPosition, buildingGridPosition.VerticalGridPosition, this.buildingConfiguration); ;

            base.EnableBuildingOutline(base.GetSquare(buildingGridPosition.HorizontalGridPosition, buildingGridPosition.VerticalGridPosition), this.buildingConfiguration);

            // Debug.LogFormat("Line : {0}, Column {1}", vertical, horizontal);
        }
        else
        {
            this.buildingToCreateGameObject.transform.position = this.ray.origin + this.ray.direction * 7.5f;

            base.DisableAllConstructionSquaresOutline();
            this.canPlaceTheBuildingOnGrid = false;
        }
    }

    /// <summary>
    /// Détruire le bâtiment que l'on souhaité placé puis construire.
    /// </summary>
    public void DestroyBuildingToBuild()
    {
        ServiceLocator.Instance.ObjectsPoolManager.RemoveObjectInPool(this.buildingName, buildingToCreateGameObject);
    }
    
    /// <summary>
    /// Détermine si il est possible de détruire le bâtiment que l'on souhaité construire.
    /// </summary>
    /// <returns></returns>
    public bool CanDestroyBuildingToBuild()
    {
        return null != this.buildingToCreateGameObject;
    }

    /// <summary>
    /// Détruit le bâtiment que l'on souhaité construire si cela est possible.
    /// </summary>
    public void DesotryBuildingToBuildingIfPossible()
    {
        if (this.CanDestroyBuildingToBuild())
            this.DestroyBuildingToBuild();
    }

    /// <summary>
    /// Instancie le bâtiment en donnant son nom à l'object pool manageur et initialize les données que l'on a besoin pour pouvoir le traiter.
    /// </summary>
    /// <param name="buildingName"></param>
    public void InstantiateBuilding(string buildingName)
    {
        this.buildingName = buildingName;
        this.buildingConfiguration = ServiceLocator.Instance.BuildingsConfiguration.GetConfiguration(this.buildingName);

        this.DesotryBuildingToBuildingIfPossible();

        this.buildingToCreateGameObject = ServiceLocator.Instance.ObjectsPoolManager.AddObjectInPool(this.buildingName);
        this.buildingToCreateGameObject.transform.localPosition = Vector3.zero;
    }
    #endregion
}
