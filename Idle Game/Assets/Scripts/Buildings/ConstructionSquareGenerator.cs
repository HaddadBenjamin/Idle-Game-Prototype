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

        // Parcour vertical
        for (int boardVerticalIndex = 0; boardVerticalIndex < this.boardVertical; boardVerticalIndex++)
        {
            // Parcour horizontal
            for (int boardHorizontalIndex = 0; boardHorizontalIndex < this.boardHorizontal; boardHorizontalIndex++)
            {
                GameObject constructionSquare = ServiceLocator.Instance.ObjectsPoolManager.AddObjectInPool("ConstructionSquare");

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

        ServiceLocator.Instance.EventManagerParamsConstructionSquareArrayAndInt.CallEvent(
            EEventParamsConstructionSquareArrayAndInt.FinishToGenerateConstructionSquare,
            constructionSquares, 
            this.boardHorizontal);
    }
}
