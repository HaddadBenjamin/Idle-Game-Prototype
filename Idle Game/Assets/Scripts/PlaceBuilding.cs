using UnityEngine;
using System.Collections;

// Ce script doit être dans le playerBuildingPlacement ??
public class PlaceBuilding : MonoBehaviour
{
    private Transform myTransform;
    private Ray ray;
    private RaycastHit hit;
    private ConstructionBuildingParameters constructionBuildingParameters;
    private float movementInterpolationSpeed = 1.0f;
    private float objectDistanceFromCenterOfScreen = 7.5f;
    private LayerMask raycastLayer;
    private ConstructionSquareGenerator constructionSquareGenerator;

    void Awake()
    {
        this.myTransform = transform;
        this.constructionBuildingParameters = GetComponent<ConstructionBuildingParameters>();
        this.raycastLayer = LayerMask.GetMask("ConstructionSquare");

        this.constructionSquareGenerator =
                GameObject.FindGameObjectWithTag("ServiceLocator").
                GetComponent<ServiceLocator>().
                GameObjectReferenceManager.Get("Construction Square Generator").
                GetComponent<ConstructionSquareGenerator>();
    }

	void Update ()
    {
        this.PlaceBuildingAndAffectOutlineOfConstructionSquares();
	}

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

            this.myTransform.position = newBuildingPosition;
            this.constructionSquareGenerator.ShowBuildingOutline(
                this.constructionSquareGenerator.GetSquare(buildingHorizontal, buildingVertical), 
                this.constructionBuildingParameters);
            // Debug.LogFormat("Line : {0}, Column {1}", vertical, horizontal);
        }
        else
        {
            this.myTransform.position = this.ray.origin + this.ray.direction * this.objectDistanceFromCenterOfScreen;

            this.constructionSquareGenerator.UnshowConstructionSquaresOutline();
        }
    }

    private Vector3 GetNewBuildingPosition(Transform colliderTransform, int horizontal, int vertical)
    {
        Vector3 newBuildingPosition = this.constructionSquareGenerator.GetSquare(horizontal, vertical).transform.position;

        newBuildingPosition.y += colliderTransform.lossyScale.y;
        newBuildingPosition.x += colliderTransform.lossyScale.x * this.constructionBuildingParameters.HorizontalOffsetNormalized;
        newBuildingPosition.z -= colliderTransform.lossyScale.z * this.constructionBuildingParameters.VerticalOffsetNormalized;

        return newBuildingPosition;
    }
}
