using UnityEngine;
using System.Collections;

/// <summary>
/// Intéractions sur les cases de constructions.
/// </summary>
public abstract class ABuildingManager : MonoBehaviour
{
    #region Fields
    /// <summary>
    /// Les cases de constructions sur lequel agit ce manageur de bâtiment.
    /// </summary>
    private ConstructionSquare[] constructionSquares;
    private int boardHorizontal;
    #endregion

    #region Unity Functions
    protected void MyAwake()
    {
        ServiceLocator.Instance.
            GameObjectReferenceManager.Get("Construction Square Generator").
            GetComponent<ConstructionSquareGenerator>().
            FinishToGenerateDelegate += this.SetConstructionSquaresAndBoardHorizontal;
    }
    #endregion

    #region Behaviour Methods

    #region Get & Set
    /// <summary>
    /// Permet de setter les cases sur lequel va agir ce manageur de bâtiment.
    /// </summary>
    /// <param name="constructionSquares"></param>
    public void SetConstructionSquaresAndBoardHorizontal(ConstructionSquare[] constructionSquares, int boardHorizontal)
    {
        this.constructionSquares = constructionSquares;
        this.boardHorizontal = boardHorizontal;
    }


    /// <summary>
    /// DANGEREUX : voir le ConstructionSquareGenerator.
    /// </summary>
    /// <param name="horizontal"></param>
    /// <param name="vertical"></param>
    /// <returns></returns>
    public int GetPosition(int horizontal, int vertical)
    {
        // Revoir cette méthode, elle me paraît pas logique
        return horizontal + vertical * this.boardHorizontal;
    }

    /// <summary>
    /// Retourne la case de construction au positions demandées.
    /// </summary>
    /// <param name="horizontal"></param>
    /// <param name="vertical"></param>
    /// <returns></returns>
    public ConstructionSquare GetSquare(int horizontal, int vertical)
    {
        return this.constructionSquares[this.GetPosition(horizontal, vertical)];
    }

    /// <summary>
    /// Retourne la case de construction après quelle soit été traité dans la grille de sorte qu'elle ne les dépassent pas.
    /// </summary>
    /// <param name="constructionSquare"></param>
    /// <param name="constructionBuildingParameters"></param>
    /// <returns></returns>
    public ConstructionSquare GetConstructionSquareWithoutGridOverflow(ConstructionSquare constructionSquare, BuildingConfiguration buildingConfiguration)
    {
        int buildingVertical = buildingConfiguration.GetGridVerticalPositionWithoutOverflow(constructionSquare.VerticalPositionInGrid);
        int buildingHorizontal = buildingConfiguration.GetGridHorizontalPositionWithoutOverflow(constructionSquare.HorizontalPositionInGrid);

        return this.GetSquare(buildingHorizontal, buildingVertical);
    }

    #endregion

    #region Outline
    /// <summary>
    /// Active un effet sur les cases d'un bâtiment.
    /// </summary>
    /// <param name="constructionSquare"></param>
    /// <param name="constructionBuildingParameters"></param>
    public void EnableBuildingOutline(ConstructionSquare constructionSquare, BuildingConfiguration buildingConfiguration)
    {
        this.DisableAllConstructionSquaresOutline();

        for (int boardVerticalIndex = constructionSquare.VerticalPositionInGrid;
            boardVerticalIndex >= constructionSquare.VerticalPositionInGrid - buildingConfiguration.VerticalLenght + 1;
            boardVerticalIndex--)
        {
            for (int boardHorizontalIndex = constructionSquare.HorizontalPositionInGrid;
                boardHorizontalIndex >= constructionSquare.HorizontalPositionInGrid - buildingConfiguration.HorizontalLenght + 1;
                boardHorizontalIndex--)
                this.constructionSquares[this.GetPosition(boardHorizontalIndex, boardVerticalIndex)].ShowOutline = true;
        }
    }

    /// <summary>
    /// Désactives un effet sur toute les cases de construction.
    /// </summary>
    public void DisableAllConstructionSquaresOutline()
    {
        for (int constructionSqareIndex = 0; constructionSqareIndex < this.constructionSquares.Length; constructionSqareIndex++)
            this.constructionSquares[constructionSqareIndex].ShowOutline = false;
    }
    #endregion

    #region Add / Remove Buildings
    /// <summary>
    /// Détermine si les cases d'un où l'on souhaite placé un bâtiment sont tous en mode constructable.
    /// </summary>
    /// <param name="constructionSquare"></param>
    /// <param name="constructionBuildingParameters"></param>
    /// <returns></returns>
    public bool DoesItIsPossibleToBuildABuildingOnThisArea(ConstructionSquare constructionSquare, BuildingConfiguration buildingConfiguration)
    {
        ConstructionSquare processedConstructionSquare = this.GetConstructionSquareWithoutGridOverflow(constructionSquare, buildingConfiguration);

        for (int boardVerticalIndex = processedConstructionSquare.VerticalPositionInGrid;
            boardVerticalIndex >= processedConstructionSquare.VerticalPositionInGrid - buildingConfiguration.VerticalLenght + 1;
            boardVerticalIndex--)
        {
            for (int boardHorizontalIndex = processedConstructionSquare.HorizontalPositionInGrid;
                boardHorizontalIndex >= processedConstructionSquare.HorizontalPositionInGrid - buildingConfiguration.HorizontalLenght + 1;
                boardHorizontalIndex--)
            {
                if (this.constructionSquares[this.GetPosition(boardHorizontalIndex, boardVerticalIndex)].DoesThereIsABuilding)
                    return false;
            }
        }

        return true;
    }

    /// <summary>
    /// Défini toutes les cases où veut se placer un bâtiment en constructable.
    /// </summary>
    /// <param name="constructionSquare"></param>
    /// <param name="constructionBuildingParameters"></param>
    public void PutThisAreaAsConstructible(ConstructionSquare constructionSquare, BuildingConfiguration buildingConfiguration)
    {
        this.ModifyTheConstructibilityOfAnArea(constructionSquare, buildingConfiguration, false);
    }

    /// <summary>
    /// Défini toutes les cases où veut se placer un bâtiment en non constructable.
    /// </summary>
    /// <param name="constructionSquare"></param>
    /// <param name="constructionBuildingParameters"></param>
    public void PutThisAreaAsUnconstructible(ConstructionSquare constructionSquare, BuildingConfiguration buildingConfiguration)
    {
        this.ModifyTheConstructibilityOfAnArea(constructionSquare, buildingConfiguration, true);
    }

    /// <summary>
    /// Modifie le mode constructable de toutes les cases où veut se placer un bâtiment.
    /// </summary>
    /// <param name="constructionSquare"></param>
    /// <param name="constructionBuildingParameters"></param>
    /// <param name="thereIsABuildingHere"></param>
    private void ModifyTheConstructibilityOfAnArea(ConstructionSquare constructionSquare, BuildingConfiguration buildingConfiguration, bool thereIsABuildingHere)
    {
        ConstructionSquare processedConstructionSquare = this.GetConstructionSquareWithoutGridOverflow(constructionSquare, buildingConfiguration);

        for (int boardVerticalIndex = processedConstructionSquare.VerticalPositionInGrid;
           boardVerticalIndex >= processedConstructionSquare.VerticalPositionInGrid - buildingConfiguration.VerticalLenght + 1;
           boardVerticalIndex--)
        {
            for (int boardHorizontalIndex = processedConstructionSquare.HorizontalPositionInGrid;
                boardHorizontalIndex >= processedConstructionSquare.HorizontalPositionInGrid - buildingConfiguration.HorizontalLenght + 1;
                boardHorizontalIndex--)
                this.constructionSquares[this.GetPosition(boardHorizontalIndex, boardVerticalIndex)].DoesThereIsABuilding = thereIsABuildingHere;
        }
    }

    /// <summary>
    ///  Permet de connaître la position du bâtiment en fonction de la grille de sorte qu'elle ne déborde pas sur cette dernière.
    /// </summary>
    /// <param name="colliderTransform"></param>
    /// <param name="horizontal"></param>
    /// <param name="vertical"></param>
    /// <returns></returns>
    public Vector3 GetNewBuildingPosition(int horizontal, int vertical, BuildingConfiguration buildingConfiguration)
    {
        Transform squareTransform = this.GetSquare(horizontal, vertical).transform;
        Vector3 newBuildingPosition = squareTransform.position;

        newBuildingPosition.y += squareTransform.lossyScale.y;
        newBuildingPosition.x += squareTransform.lossyScale.x * buildingConfiguration.HorizontalOffsetNormalized;
        newBuildingPosition.z -= squareTransform.lossyScale.z * buildingConfiguration.VerticalOffsetNormalized;

        return newBuildingPosition;
    }
    #endregion

    #endregion
}
