using UnityEngine;
using System.Collections;

public class ConstructionSquareGenerator : MonoBehaviour
{
    #region Fields
    [SerializeField]
    private int boardHorizontal = 8; // largeur, vertical, colonne
    [SerializeField]
    private int boardVertical = 8; // longueur, horizontal, line

    public delegate void Delegate(ConstructionSquare[] constructionSquares, int boardHorizontal);
    public Delegate FinishToGenerateDelegate;
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
        GameObject  constructionSquareGameObject = ServiceLocator.Instance.GameObjectManager.Get("ConstructionSquare");

        // Parcour vertical
        for (int boardVerticalIndex = 0; boardVerticalIndex < this.boardVertical; boardVerticalIndex++)
        {
            // Parcour horizontal
            for (int boardHorizontalIndex = 0; boardHorizontalIndex < this.boardHorizontal; boardHorizontalIndex++)
            {
                GameObject constructionSquare = GameObject.Instantiate(constructionSquareGameObject);
                Transform constructionSquareTransform = constructionSquare.transform;
                ConstructionSquare constructionSquareScript = constructionSquare.AddComponent<ConstructionSquare>();

                // Défini un numéro de ligne et de colonne à chaque case de construction, ceci permettra de mieu placer les bâtiments dessus.
                constructionSquareScript.Initialize(boardHorizontalIndex, boardHorizontalIndex);
                // DANGEREUX : Correspond à ABuildManager.GetPosition
                constructionSquares[boardHorizontalIndex + boardVerticalIndex * this.boardHorizontal] = constructionSquareScript;

                constructionSquareTransform.parent = myTransform;
                constructionSquareTransform.localPosition =
                    new Vector3(boardHorizontalIndex * constructionSquareTransform.lossyScale.x,
                    0.0f,
                    -boardVerticalIndex * constructionSquareTransform.lossyScale.z);
            }
        }

        if (null != this.FinishToGenerateDelegate)
            this.FinishToGenerateDelegate(constructionSquares, this.boardHorizontal);
    }
}
