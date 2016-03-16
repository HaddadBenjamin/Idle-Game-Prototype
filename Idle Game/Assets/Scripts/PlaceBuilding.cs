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
        this.ray = Camera.main.ScreenPointToRay(Input.mousePosition);

        if (Physics.Raycast(this.ray, out this.hit, this.raycastLayer))
        {
            Transform colliderTransform = this.hit.collider.transform;
            ConstructionSquare constructionSquare = colliderTransform.GetComponent<ConstructionSquare>();

            int squareVertical = constructionSquare.CellVertical;
            int squareHorizontal = constructionSquare.CellHorizontal;

            int vertical = squareVertical;
            int horizontal = squareHorizontal;

            if (horizontal < this.constructionBuildingParameters.HorizontalLenght - 1)
                horizontal = this.constructionBuildingParameters.HorizontalLenght - 1;
            if (vertical < this.constructionBuildingParameters.VerticalLenght - 1)
                vertical = this.constructionBuildingParameters.VerticalLenght - 1;

            // Debug.LogFormat("Line : {0}, Column {1}", vertical, horizontal);

            Vector3 newBuildingPosition = constructionSquareGenerator.GetSquare(horizontal, vertical).transform.position;

            newBuildingPosition.y += colliderTransform.lossyScale.y;
            newBuildingPosition.x += colliderTransform.lossyScale.x * this.constructionBuildingParameters.HorizontalOffsetNormalized;
            newBuildingPosition.z -= colliderTransform.lossyScale.z * this.constructionBuildingParameters.VerticalOffsetNormalized;

            this.myTransform.position = newBuildingPosition;

            constructionSquareGenerator.ShowBuildingOutline(
                constructionSquareGenerator.GetSquare(horizontal, vertical), constructionBuildingParameters);
        }
        else
        {
            this.myTransform.position = this.ray.origin + this.ray.direction * this.objectDistanceFromCenterOfScreen;
            constructionSquareGenerator.UnshowConstructionSquaresOutline();
        }
	}
}
