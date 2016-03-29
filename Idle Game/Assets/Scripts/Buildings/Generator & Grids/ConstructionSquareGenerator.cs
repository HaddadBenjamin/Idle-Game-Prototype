using UnityEngine;
using System.Collections;

public class ConstructionSquareGenerator : MonoBehaviour
{
    #region Fields
    [SerializeField]
    private int boardHorizontal = 8; // largeur, vertical, colonne
    [SerializeField]
    private int boardVertical = 8; // longueur, horizontal, line
    #endregion

    #region Properties
    public int BoardHorizontal
    {
        get { return boardHorizontal; }
        private set { boardHorizontal = value; }
    }

    public int BoardVertical
    {
        get { return boardVertical; }
        private set { boardVertical = value; }
    }
    #endregion

    #region Unity Methods
    void Start()
    {
        this.GenerateConstructionSquares();
    }
    #endregion

    private void GenerateConstructionSquares()
    {
        Transform myTransform = transform;
        ConstructionSquare[] constructionSquares = new ConstructionSquare[this.boardHorizontal * this.boardVertical];

        this.GenerateTerrain(myTransform);

        // Parcour vertical
        for (int boardVerticalIndex = 0; boardVerticalIndex < this.boardVertical; boardVerticalIndex++)
        {
            // Parcour horizontal
            for (int boardHorizontalIndex = 0; boardHorizontalIndex < this.boardHorizontal; boardHorizontalIndex++)
            {
                GameObject constructionSquare = ServiceContainer.Instance.ObjectsPoolManager.AddObjectInPool("ConstructionSquare");

                Transform constructionSquareTransform = constructionSquare.transform;
                ConstructionSquare constructionSquareScript = constructionSquare.AddComponent<ConstructionSquare>();

                // Défini un numéro de ligne et de colonne à chaque case de construction, ceci permettra de mieu placer les bâtiments dessus.
                constructionSquareScript.Initialize(boardHorizontalIndex, boardVerticalIndex);
                // DANGEREUX : Correspond à ABuildManager.GetPosition
                constructionSquares[boardHorizontalIndex + boardVerticalIndex * this.boardHorizontal] = constructionSquareScript;

                constructionSquareTransform.parent = myTransform;
                constructionSquareTransform.localPosition =
                    new Vector3(boardHorizontalIndex * constructionSquareTransform.lossyScale.x,
                    0.0f,
                    -boardVerticalIndex * constructionSquareTransform.lossyScale.z);
            }
        }

        ServiceContainer.Instance.EventManagerParamsConstructionSquareArrayAndInt.CallEvent(
            EEventParamsConstructionSquareArrayAndInt.FinishToGenerateConstructionSquare,
            constructionSquares, 
            this.boardHorizontal);
    }

    private void GenerateTerrain(Transform parent)
    {
        GameObject terrain = ServiceContainer.Instance.ObjectsPoolManager.AddObjectInPool("ConstructionSquare");
        Transform terrainTransform = terrain.transform;

        terrainTransform.parent = parent;

        terrainTransform.localPosition = new Vector3((this.boardHorizontal - 1) * 0.5f * terrainTransform.lossyScale.x,
            -0.1f,
            -(this.boardVertical - 1) * 0.5f * terrainTransform.lossyScale.z); 
        terrainTransform.localScale = new Vector3(this.boardHorizontal, 1.0f, this.boardVertical);


        terrain.GetComponent<Renderer>().material = ServiceContainer.Instance.MaterialReferences.Get("Wood");
        terrain.GetComponent<BoxCollider>().enabled = false;

        ServiceContainer.Instance.EventManagerParamsVector3.CallEvent(
            EEventParamsVector3.ConstructionSquareHaveBeenGeneratedHereTheCenterPosition, 
            new Vector3(terrainTransform.position.x, terrainTransform.position.y + 10.0f, terrainTransform.position.z));

    }
}
