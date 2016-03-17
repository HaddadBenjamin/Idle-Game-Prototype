using UnityEngine;
using System.Collections;

public class ConstructionSquareGenerator : MonoBehaviour
{
    #region Fields
    [SerializeField]
    private int boardHorizontal = 8; // largeur, vertical, colonne
    [SerializeField]
    private int boardVertical = 8; // longueur, horizontal, line
    private GameObject constructionSquareGameObject;
    private Transform myTransform;
    private ConstructionSquare[] constructionSquares;
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
        this.constructionSquareGameObject =
            GameObject.FindGameObjectWithTag("ServiceLocator").
            GetComponent<ServiceLocator>().
            GameObjectManager.Get("ConstructionSquare");

        this.myTransform = transform;

        this.constructionSquares = new ConstructionSquare[this.boardHorizontal * this.boardVertical];

        this.GenerateConstructionSquares();
    }
    #endregion

    #region Behaviour
    public ConstructionSquare GetSquare(int horizontal, int vertical)
    {
        return this.constructionSquares[horizontal + vertical * this.boardVertical];
    }

    public ConstructionSquare GetProcessedConstructionSquare(ConstructionSquare constructionSquare, ConstructionBuildingParameters constructionBuildingParameters)
    {
        int buildingVertical = constructionBuildingParameters.GetBuildingVertical(constructionSquare.CellVertical);
        int buildingHorizontal = constructionBuildingParameters.GetBuildingHorizontal(constructionSquare.CellHorizontal);

        return this.GetSquare(buildingHorizontal, buildingVertical);
    }

    public void ShowBuildingOutline(ConstructionSquare constructionSquare, ConstructionBuildingParameters constructionBuildingParameters)
    {
        this.UnshowConstructionSquaresOutline();

        for (int boardVerticalIndex = constructionSquare.CellVertical;
            boardVerticalIndex >= constructionSquare.CellVertical - constructionBuildingParameters.VerticalLenght + 1;
            boardVerticalIndex--)
        {
            for (int boardHorizontalIndex = constructionSquare.CellHorizontal;
                boardHorizontalIndex >= constructionSquare.CellHorizontal - constructionBuildingParameters.HorizontalLenght + 1;
                boardHorizontalIndex--)
                this.constructionSquares[this.GetPosition(boardHorizontalIndex, boardVerticalIndex)].ShowOutline = true;
        }
    }

    public bool CanBuildHere(ConstructionSquare constructionSquare, ConstructionBuildingParameters constructionBuildingParameters)
    {
        ConstructionSquare processedConstructionSquare = this.GetProcessedConstructionSquare(constructionSquare, constructionBuildingParameters);

        for (int boardVerticalIndex = processedConstructionSquare.CellVertical;
            boardVerticalIndex >= processedConstructionSquare.CellVertical - constructionBuildingParameters.VerticalLenght + 1;
            boardVerticalIndex--)
        {
            for (int boardHorizontalIndex = processedConstructionSquare.CellHorizontal;
                boardHorizontalIndex >= processedConstructionSquare.CellHorizontal - constructionBuildingParameters.HorizontalLenght + 1;
                boardHorizontalIndex--)
            {
                if (this.constructionSquares[this.GetPosition(boardHorizontalIndex, boardVerticalIndex)].ThereIsABuildingHere)
                    return false;
            }
        }

        return true;
    }

    public void DestroyBuilding(ConstructionSquare constructionSquare, ConstructionBuildingParameters constructionBuildingParameters)
    {
        this.ModifyThereIsABuildingHere(constructionSquare, constructionBuildingParameters, false);
    }

    public void AddBuilding(ConstructionSquare constructionSquare, ConstructionBuildingParameters constructionBuildingParameters)
    {
        this.ModifyThereIsABuildingHere(constructionSquare, constructionBuildingParameters, true);
    }

    private void ModifyThereIsABuildingHere(ConstructionSquare constructionSquare, ConstructionBuildingParameters constructionBuildingParameters, bool thereIsABuildingHere)
    {
        ConstructionSquare processedConstructionSquare = this.GetProcessedConstructionSquare(constructionSquare, constructionBuildingParameters);

        for (int boardVerticalIndex = processedConstructionSquare.CellVertical;
           boardVerticalIndex >= processedConstructionSquare.CellVertical - constructionBuildingParameters.VerticalLenght + 1;
           boardVerticalIndex--)
        {
            for (int boardHorizontalIndex = processedConstructionSquare.CellHorizontal;
                boardHorizontalIndex >= processedConstructionSquare.CellHorizontal - constructionBuildingParameters.HorizontalLenght + 1;
                boardHorizontalIndex--)
                this.constructionSquares[this.GetPosition(boardHorizontalIndex, boardVerticalIndex)].ThereIsABuildingHere = thereIsABuildingHere;
        }
    }

    public void UnshowConstructionSquaresOutline()
    {
        for (int constructionSqareIndex = 0; constructionSqareIndex < this.constructionSquares.Length; constructionSqareIndex++)
            this.constructionSquares[constructionSqareIndex].ShowOutline = false;
    }

    private int GetPosition(int horizontal, int vertical)
    {
        return horizontal + vertical * this.boardHorizontal;
    }

    private void GenerateConstructionSquares()
    {
        // Parcour vertical
        for (int boardVerticalIndex = 0; boardVerticalIndex < this.boardVertical; boardVerticalIndex++)
        {
            // Parcour horizontal
            for (int boardHorizontalIndex = 0; boardHorizontalIndex < this.boardHorizontal; boardHorizontalIndex++)
            {
                GameObject constructionSquare = GameObject.Instantiate(this.constructionSquareGameObject);
                Transform constructionSquareTransform = constructionSquare.transform;
                ConstructionSquare constructionSquareScript = constructionSquare.GetComponent<ConstructionSquare>();

                this.constructionSquares[this.GetPosition(boardHorizontalIndex, boardVerticalIndex)] = constructionSquareScript;

                constructionSquareTransform.parent = myTransform;
                constructionSquareTransform.localPosition =
                    new Vector3(boardHorizontalIndex * constructionSquareTransform.lossyScale.x,
                    0.0f,
                    -boardVerticalIndex * constructionSquareTransform.lossyScale.z);

                // Défini un numéro de ligne et de colonne à chaque case de construction, ceci permettra de mieu placer les bâtiments dessus.
                constructionSquareScript.CellHorizontal = boardHorizontalIndex;
                constructionSquareScript.CellVertical = boardVerticalIndex;
            }
        }
    }
    #endregion
}
