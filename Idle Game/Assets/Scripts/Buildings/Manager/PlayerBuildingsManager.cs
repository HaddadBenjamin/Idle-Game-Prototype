using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public enum EBuildingManagerMode
{
    Construction,
    SelectBuilding,
    Move,
}

//construction Square, cellule, taillee
public class PlayerBuildingsManager : ABuildingManager
{
    #region Fields
    /// <summary>
    /// Lorsque l'on pose le bâtiment on doit déterminer si le joueur peut le payer et si cest le cas, alors le payer.
    /// </summary>
    private PlayerResources playerResources;
    public MenusAnimations menusAnimations;

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
    private GameObject buildingGameObject;
    /// <summary>
    /// Le nom de la prefab du bâtiment à instancier / détruire (utilisation de l'object pool).
    /// </summary>
    private string buildingName;

    /// <summary>
    /// Nous permet de placer le bâtiment sur les cases.
    /// </summary>
    private Ray ray;
    private RaycastHit hit;
    private bool raycastHitWithBuilding;

    /// <summary>
    /// Si notre rayon ne trouve pas de cases cette condition sinon elle est fausse.
    /// </summary>
    private bool canPlaceTheBuildingOnGrid;

    private List<ABuilding> Buildings;
    public EBuildingManagerMode BuildingManagerMode { get; private set; }
    private ConstructionSquare constructionSquareForMoveAndRotateTemporary;

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
        this.Buildings = new List<ABuilding>();

        this.BuildingManagerMode = EBuildingManagerMode.Construction;
    }

    void Start()
    {
        this.playerResources = GetComponent<PlayerResources>();

        this.buildingsAnalytic.AtStartUpdateAllMembersSubscribeToDelegateAfterInitialization();

        ServiceLocator.Instance.EventManager.SubcribeToEvent(EEvent.DestroyBuildingToBuild, this.DestroyBuildingToBuild);
        this.menusAnimations = ServiceLocator.Instance.GameObjectReferenceManager.Get("Canvas").GetComponent<MenusAnimations>();
    }

    void Update()
    {
        if (EMenuAnimation.ResourceConstruction == this.menusAnimations.CurrentMenuAnimation)
        {
            if (null != this.buildingGameObject)
            {
                this.PlaceBuildingAndAffectOutlineOfConstructionSquares();

                if (Input.GetMouseButtonDown(0))
                    this.AddBuilding();
            }
        }
        
        else if (EMenuAnimation.Default == this.menusAnimations.CurrentMenuAnimation)
        {
            this.RaycastOnBuildingLayer();

            if (this.hit.collider &&  this.hit.collider.gameObject.layer == LayerMask.NameToLayer("Building") && Input.GetMouseButtonDown(0))
            {
                this.buildingGameObject = this.hit.collider.gameObject;

                Debug.Log(buildingGameObject);
                Debug.Log("select building & call event click on building");

                ServiceLocator.Instance.EventManager.CallEvent(EEvent.ClickOnBuilding);
            }
        }
        else if (EMenuAnimation.BuildingInteractions == this.menusAnimations.CurrentMenuAnimation)
        {
            if (EBuildingManagerMode.Move == this.BuildingManagerMode)
            {
                this.PlaceBuildingAndAffectOutlineOfConstructionSquares();

                this.PlaceBuildingOnClickAfterMoveOrRotateInteractions();
            }
        }
    }

    private void PlaceBuildingOnClickAfterMoveOrRotateInteractions()
    {
        if (Input.GetMouseButtonDown(0) && this.canPlaceTheBuildingOnGrid && base.DoesItIsPossibleToBuildABuildingOnThisArea(this.constructionSquare, this.buildingConfiguration))
            this.PlaceBuildingAfterMoveOrRotate();
    }

    private void PlaceBuildingAfterMoveOrRotate()
    {
        this.buildingGameObject.GetComponent<ABuilding>().ConstructionSquareReference = this.constructionSquare;
        
        GridPosition buildingGridPosition = this.buildingConfiguration.GetGridPositionWithoutOverflow(constructionSquare.HorizontalPositionInGrid, constructionSquare.VerticalPositionInGrid);
        this.buildingGameObject.transform.position = base.GetNewBuildingPosition(buildingGridPosition.HorizontalGridPosition, buildingGridPosition.VerticalGridPosition, this.buildingConfiguration); ;

        this.buildingGameObject = null;
        base.DisableAllConstructionSquaresOutline();
        base.PutThisAreaAsUnconstructible(this.constructionSquare, this.buildingConfiguration);

        this.BuildingManagerMode = EBuildingManagerMode.Construction;
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
                        if (this.playerResources.HaveEnoughtResource(this.buildingConfiguration.GetLevelConfiguration(1).Price))
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
            ABuilding building = this.buildingGameObject.GetComponent<ABuilding>();

            building.ConstructionSquareReference = this.constructionSquare;

            // Permet la génération de resources, au destroy il faudra penser à faire l'inverse
            if (EBuildingCategory.ResourceConstruction == building.BuildingCategory)
                (building as IndustryBuilding).InitializeResourceGeneration(this.buildingName);

            this.Buildings.Add(building);

            // Pas besoin ici de faire un objectPool.RemoveInPool
            // Parcontre on devrait ici rajouter dans une List<APlayerBuilding>(buildingToCreateGameObject, cellData, priceData, etc..);            
            this.buildingGameObject = null;

            base.DisableAllConstructionSquaresOutline();
            base.PutThisAreaAsUnconstructible(this.constructionSquare, this.buildingConfiguration);

            this.buildingsAnalytic.GetConstructionBuildings(this.buildingConfiguration.IndustryCategory).Add();
            this.buildingsAnalytic.PiecesOfFurniture.Add();

            this.playerResources.Pay(this.buildingConfiguration.GetLevelConfiguration(1).Price);
        }

        return canAddBuilding;
    }

    /// <summary>
    /// Permet de positionner un bâtiment sur la grille de cases de constructions et active / désactive le outline sur ces cases.
    /// </summary>
    private void PlaceBuildingAndAffectOutlineOfConstructionSquares()
    {
        this.ray = Camera.main.ScreenPointToRay(Input.mousePosition);

        bool raycastWorked = false;
        ///Les masques ne marchent pas j'ai le seum !!!!'
        if (Physics.Raycast(this.ray, out this.hit, (1 << LayerMask.NameToLayer("ConstructionSquare"))))
        {
            raycastWorked = (this.hit.collider.gameObject.layer == LayerMask.NameToLayer("ConstructionSquare"));

            //Debug.Log("raycast worked : " + raycastWorked);
            if (raycastWorked)
            {
                Transform colliderTransform = this.hit.collider.transform;
                this.constructionSquare = colliderTransform.GetComponent<ConstructionSquare>();
                this.canPlaceTheBuildingOnGrid = true;
                this.buildingConfiguration = ServiceLocator.Instance.BuildingsConfiguration.GetConfiguration(this.buildingName);

                GridPosition buildingGridPosition = this.buildingConfiguration.GetGridPositionWithoutOverflow(constructionSquare.HorizontalPositionInGrid, constructionSquare.VerticalPositionInGrid);

                this.buildingGameObject.transform.position = base.GetNewBuildingPosition(buildingGridPosition.HorizontalGridPosition, buildingGridPosition.VerticalGridPosition, this.buildingConfiguration); ;

                base.EnableBuildingOutline(base.GetSquare(buildingGridPosition.HorizontalGridPosition, buildingGridPosition.VerticalGridPosition), this.buildingConfiguration);
                // Debug.LogFormat("Line : {0}, Column {1}", vertical, horizontal);
            }
            else
            {
                this.buildingGameObject.transform.position = this.ray.origin + this.ray.direction * 7.5f;

                base.DisableAllConstructionSquaresOutline();
                this.canPlaceTheBuildingOnGrid = false;
            }
        }
    }

    /// <summary>
    /// Détruire le bâtiment que l'on souhaité placé puis construire.
    /// </summary>
    public void DestroyBuildingToBuild()
    {
       Destroy(buildingGameObject);
    }
    
    /// <summary>
    /// Détermine si il est possible de détruire le bâtiment que l'on souhaité construire.
    /// </summary>
    /// <returns></returns>
    public bool CanDestroyBuildingToBuild()
    {
        return null != this.buildingGameObject;
    }

    /// <summary>
    /// Détruit le bâtiment que l'on souhaité construire si cela est possible.
    /// </summary>
    public void DestroyBuildingToBuildingIfPossible()
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

        this.DestroyBuildingToBuildingIfPossible();

        this.buildingGameObject = ServiceLocator.Instance.GameObjectManager.Instantiate(this.buildingName);
        this.buildingGameObject.transform.localPosition = Vector3.zero;
    }

    private void RaycastOnBuildingLayer()
    {
        this.ray = Camera.main.ScreenPointToRay(Input.mousePosition);

        Physics.Raycast(this.ray, out this.hit, (1 << LayerMask.NameToLayer("Building")));
    }

    public void MoveSelectedBuilding()
    {
        this.BuildingManagerMode = EBuildingManagerMode.Move;

        this.SelectBuildingPreInteractionsForMoveRotateSellDestroy();
    }

    private void SelectBuildingPreInteractionsForMoveRotateSellDestroy()
    {
        ABuilding building = this.buildingGameObject.GetComponent<ABuilding>();

        this.buildingName = building.BuildingName;
        this.buildingConfiguration = ServiceLocator.Instance.BuildingsConfiguration.GetConfiguration(this.buildingName);
        this.constructionSquare = building.ConstructionSquareReference;
        this.constructionSquareForMoveAndRotateTemporary = this.constructionSquare;
        this.buildingGameObject.transform.localPosition = Vector3.zero;

        base.PutThisAreaAsConstructible(this.constructionSquare, this.buildingConfiguration);
    }

    public void CancelBuildingMove()
    {
        this.constructionSquare = this.constructionSquareForMoveAndRotateTemporary;

        this.PlaceBuildingAfterMoveOrRotate();
    }
    
    public void LevelUpSelectedBuilding()
    {
        ABuilding building = this.buildingGameObject.GetComponent<ABuilding>();

        // Dangereux.
        bool canLevelUp = (building as IndustryBuilding).LevelUpIfPossible();

        if (canLevelUp) // Dangereux
            Debug.Log("Building " + (building as IndustryBuilding).ConstructionBuildingCategory + " is now level " + (building as IndustryBuilding).BuildingLevel);
        else
            Debug.Log("This building is already to max level");
    }

    public void RemoveSelectedBuilding()
    {
        this.SelectBuildingPreInteractionsForMoveRotateSellDestroy(); 
        
        ABuilding building = this.buildingGameObject.GetComponent<ABuilding>();

        this.Buildings.Remove(building);

        base.DisableAllConstructionSquaresOutline();
        base.PutThisAreaAsConstructible(this.constructionSquare, this.buildingConfiguration);

        this.buildingsAnalytic.GetConstructionBuildings(this.buildingConfiguration.IndustryCategory).Remove();
        this.buildingsAnalytic.PiecesOfFurniture.Remove();

        // Dangereux
        (building as IndustryBuilding).UnGenerateAllResources();

        Destroy(this.buildingGameObject);
    }

    public void SellSelectedBuilding()
    {
        this.RemoveSelectedBuilding();

        ABuilding building = this.buildingGameObject.GetComponent<ABuilding>();

        building.Sell();
    }
    #endregion
}
