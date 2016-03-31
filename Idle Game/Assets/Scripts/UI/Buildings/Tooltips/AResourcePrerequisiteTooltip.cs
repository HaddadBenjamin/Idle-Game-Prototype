using UnityEngine;

using UnityEngine;
using UnityEngine.UI;

public abstract class AResourcePrerequisiteTooltip : MonoBehaviour
{
    #region Fields
    /// <summary>
    /// Ma classe utilitaire pour placer simplement des éléments dans une grille.
    /// </summary>
    [SerializeField]
    protected UIGrid grid;
    /// <summary>
    /// Nombre d'élements dans la grille, 
    /// </summary>
    [SerializeField]
    public int numberOfElementsDisplayed = 6;
    protected GameObject[] resourcePrerequisGameObject;
    protected ResourcePrerequisite[] resourcePrerequisite;

    protected PlayerResources playerResources;
    protected RectTransform rectTransform;
    #endregion

    #region Initializer
    public GameObject Initialize(RectTransform parentRectTransform)
    {
        Transform myTransform = transform;
        this.rectTransform = GetComponent<RectTransform>();
        this.playerResources = ServiceContainer.Instance.GameObjectReferencesArrayInScene.Get("[PLAYER]", EGameObjectReferences.Rest).GetComponent<PlayerResources>();
        this.resourcePrerequisGameObject = new GameObject[this.numberOfElementsDisplayed];

        for (int resourcePrerequisiteIndex = 0; resourcePrerequisiteIndex < this.resourcePrerequisGameObject.Length; resourcePrerequisiteIndex++)
        {
            this.resourcePrerequisGameObject[resourcePrerequisiteIndex] = ServiceContainer.Instance.GameObjectReferencesArrays.Instantiate(
                "Resource Equipment Prerequisite UI",
                    new Vector3(
                    this.grid.GetHorizontalPositionThanksAnIndex(resourcePrerequisiteIndex),
                    this.grid.GetVerticalPositionThanksAnIndex(resourcePrerequisiteIndex),
                    0.0f),
                Vector3.zero,
                Vector3.one,
                myTransform,
                EGameObjectReferences.UI);

            this.resourcePrerequisGameObject[resourcePrerequisiteIndex].SetActive(false);
        }

        return gameObject;
    }
    #endregion

    #region Behaviour Methods
    public abstract void SetContent(ResourcePrerequisite[] content, bool positiveColor = false);

    protected void EnableTheUsedResourcePrerequisite()
    {
        for (int resourcePrerequisiteIndex = 0; resourcePrerequisiteIndex < this.numberOfElementsDisplayed; resourcePrerequisiteIndex++)
            this.resourcePrerequisGameObject[resourcePrerequisiteIndex].SetActive(
                null == this.resourcePrerequisite ? false :
                resourcePrerequisiteIndex < this.resourcePrerequisite.Length);
    }
    #endregion
}