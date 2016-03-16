using UnityEngine;
using System.Collections;

public class PlayerBuildings : MonoBehaviour
{
    private GameObject buildingToCreateGameObject;
    private string buildingToCreateName;
    private ServiceLocator serviceLocator;

    private Transform buildingTransform;
    private Ray ray;
    private RaycastHit hit;
    private ConstructionBuildingParameters constructionBuildingParameters;
    private float movementInterpolationSpeed = 1.0f;
    private float objectDistanceFromCenterOfScreen = 7.5f;
    private LayerMask raycastLayer;
    private ConstructionSquareGenerator constructionSquareGenerator;

    public GameObject BuildingToCreateGameObject
    {
        get { return buildingToCreateGameObject; }
        private set { buildingToCreateGameObject = value; }
    }

    public string BuildingToCreateName
    {
        get { return buildingToCreateName; }
        private set { buildingToCreateName = value; }
    }

    void Start()
    {
        this.serviceLocator = GameObject.FindGameObjectWithTag("ServiceLocator").GetComponent<ServiceLocator>();

       this.raycastLayer = LayerMask.GetMask("ConstructionSquare");

        this.constructionSquareGenerator =
                GameObject.FindGameObjectWithTag("ServiceLocator").
                GetComponent<ServiceLocator>().
                GameObjectReferenceManager.Get("Construction Square Generator").
                GetComponent<ConstructionSquareGenerator>();
    }

    void Update()
    {
        if (null != this.buildingToCreateGameObject)
            this.PlaceBuildingAndAffectOutlineOfConstructionSquares();
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
        this.buildingToCreateName = buildingName;

        if (null != this.buildingToCreateGameObject)
            Destroy(buildingToCreateGameObject);

        this.buildingToCreateGameObject = this.serviceLocator.GameObjectManager.Instantiate(this.buildingToCreateName);
        this.buildingToCreateGameObject.transform.localPosition = Vector3.zero;

        this.buildingTransform = buildingToCreateGameObject.transform;
        this.constructionBuildingParameters = buildingToCreateGameObject.GetComponent<ConstructionBuildingParameters>();
    }

    public void AddBuilding()
    {
        if (null != this.buildingToCreateGameObject)
        {
            // if (PlayerBuildingContainer.PlaceBuilding(this.buildingToCreateGameObject))
            //  Destroy(this.buildingToCreateGameObject);

            // PlayerBuildingContainer.PlayerBuilding(GameObject objectToCreate)
            // bool objectExit = null != objectToCreate;
            // bool canPayObject= false;
            // if (objectExit)
            // {
            //  PlayerResource = GetComponent<PlayerResource>();
            //  BuildingDataParameter = objectToCreate.GetComponent<DataClass>();
          
            // Sous Méthode : Pay appele canPay et retourne si il a pu payé
            //  if (GetPlayerResource.CanPay(buildingDataParameter)
            //  {
            //      GetPlayerResource.Pay(buildingDataParameter);
            //      canPayObject = true;
            //  }
            //      
            // return objectExit && canPayObjct && canPlaceObject;
            // Communiquer avec if (PlayerBuildingContainer.PlaceBuilding(this.buildingToCreateGameObject);
            // Ce dernier va placer le bâtiment si il 
        }
    }
        //Follow cursor

    /// <summary>
    /// Permet de positionner un bâtiment sur la grille de cases de constructions et active / désactive le outline sur ces cases.
    /// </summary>
    private void PlaceBuildingAndAffectOutlineOfConstructionSquares()
    {
        this.ray = Camera.main.ScreenPointToRay(Input.mousePosition);

        if (Physics.Raycast(this.ray, out this.hit, this.raycastLayer))
        {
            Transform colliderTransform = this.hit.collider.transform;
            ConstructionSquare constructionSquare = colliderTransform.GetComponent<ConstructionSquare>();
            int buildingVertical = this.constructionBuildingParameters.GetBuildingVertical(constructionSquare.CellVertical);
            int buildingHorizontal = this.constructionBuildingParameters.GetBuildingHorizontal(constructionSquare.CellHorizontal);
            Vector3 newBuildingPosition = this.GetNewBuildingPosition(colliderTransform, buildingHorizontal, buildingVertical);

            this.buildingTransform.position = newBuildingPosition;
            this.constructionSquareGenerator.ShowBuildingOutline(
                this.constructionSquareGenerator.GetSquare(buildingHorizontal, buildingVertical),
                this.constructionBuildingParameters);
            // Debug.LogFormat("Line : {0}, Column {1}", vertical, horizontal);
        }
        else
        {
            this.buildingTransform.position = this.ray.origin + this.ray.direction * this.objectDistanceFromCenterOfScreen;

            this.constructionSquareGenerator.UnshowConstructionSquaresOutline();
        }
    }

    /// <summary>
    ///  Permet de connaître la position du bâtiment en fonction de la grille de sorte qu'elle ne déborde pas sur cette dernière.
    /// </summary>
    /// <param name="colliderTransform"></param>
    /// <param name="horizontal"></param>
    /// <param name="vertical"></param>
    /// <returns></returns>
    private Vector3 GetNewBuildingPosition(Transform colliderTransform, int horizontal, int vertical)
    {
        Vector3 newBuildingPosition = this.constructionSquareGenerator.GetSquare(horizontal, vertical).transform.position;

        newBuildingPosition.y += colliderTransform.lossyScale.y;
        newBuildingPosition.x += colliderTransform.lossyScale.x * this.constructionBuildingParameters.HorizontalOffsetNormalized;
        newBuildingPosition.z -= colliderTransform.lossyScale.z * this.constructionBuildingParameters.VerticalOffsetNormalized;

        return newBuildingPosition;
    }
}
