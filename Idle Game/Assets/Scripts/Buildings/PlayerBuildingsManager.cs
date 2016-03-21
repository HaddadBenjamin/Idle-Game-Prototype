using UnityEngine;
using System.Collections;

public class PlayerBuildingsManager : ABuildingManager
{
    #region Fields
    private ServiceLocator serviceLocator;
    private PlayerResources playerResources;

    private BuildingConfiguration buildingConfiguration;
    private ConstructionSquare constructionSquare;
    private GameObject buildingToCreateGameObject;

    private Ray ray;
    private RaycastHit hit;

    private bool canPlaceTheBuildingOnGrid;

    private PlayerBuildingsAnalytic buildingsAnalytic;
    #endregion

    #region Properties
    public GameObject BuildingToCreateGameObject
    {
        get { return buildingToCreateGameObject; }
        private set { buildingToCreateGameObject = value; }
    }

    public PlayerBuildingsAnalytic BuildingsAnalytic
    {
        get { return buildingsAnalytic; }
        private set { buildingsAnalytic = value; }
    }
    #endregion

    #region Unity Methods
    void Awake()
    {
        this.MyAwake();
        this.buildingsAnalytic = new PlayerBuildingsAnalytic();
    }

    void Start()
    {
        this.playerResources = GetComponent<PlayerResources>();

        this.serviceLocator = GameObject.FindGameObjectWithTag("ServiceLocator").GetComponent<ServiceLocator>();

        this.buildingsAnalytic.FirstUpdateAllMembersSubscribeToDelegateAfterInitialization();
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

    public void DestroyBuildingToBuild()
    {
        Destroy(this.buildingToCreateGameObject);
    }

    public void ActiveBuildingToCreate()
    {
        this.buildingToCreateGameObject.SetActive(true);
    }

    public void UnactiveBuildingToCreate()
    {
        this.buildingToCreateGameObject.SetActive(false);
    }

    public void InstantiateBuilding(string buildingName)
    {
        this.buildingConfiguration = this.serviceLocator.BuildingsConfiguration.GetConfiguration(buildingName);

        if (null != this.buildingToCreateGameObject)
            Destroy(buildingToCreateGameObject);

        this.buildingToCreateGameObject = this.serviceLocator.GameObjectManager.Instantiate(buildingName);
        this.buildingToCreateGameObject.transform.localPosition = Vector3.zero;
    }

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
            int buildingVertical = this.buildingConfiguration.GetGridVerticalPositionWithoutOverflow(constructionSquare.CellVertical);
            int buildingHorizontal = this.buildingConfiguration.GetGridHorizontalPositionWithoutOverflow(constructionSquare.CellHorizontal);
            Vector3 newBuildingPosition = this.GetNewBuildingPosition(buildingHorizontal, buildingVertical);

            this.buildingToCreateGameObject.transform.position = newBuildingPosition;

            base.EnableBuildingOutline(base.GetSquare(buildingHorizontal, buildingVertical), this.buildingConfiguration);

            this.canPlaceTheBuildingOnGrid = true;
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
    ///  Permet de connaître la position du bâtiment en fonction de la grille de sorte qu'elle ne déborde pas sur cette dernière.
    /// </summary>
    /// <param name="colliderTransform"></param>
    /// <param name="horizontal"></param>
    /// <param name="vertical"></param>
    /// <returns></returns>
    private Vector3 GetNewBuildingPosition(int horizontal, int vertical)
    {
        Transform squareTransform = base.GetSquare(horizontal, vertical).transform;
        Vector3 newBuildingPosition = squareTransform.position;

        newBuildingPosition.y += squareTransform.lossyScale.y;
        newBuildingPosition.x += squareTransform.lossyScale.x * this.buildingConfiguration.HorizontalOffsetNormalized;
        newBuildingPosition.z -= squareTransform.lossyScale.z * this.buildingConfiguration.VerticalOffsetNormalized;

        return newBuildingPosition;
    }
    #endregion
}
