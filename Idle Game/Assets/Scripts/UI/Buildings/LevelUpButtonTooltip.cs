using UnityEngine;
using UnityEngine.UI;

public class LevelUpButtonTooltip : MonoBehaviour
{
    #region Fields
    /// <summary>
    /// Ma classe utilitaire pour placer simplement des éléments dans une grille.
    /// </summary>
    [SerializeField]
    private UIGrid grid;
    /// <summary>
    /// Nombre d'élements dans la grille, 
    /// </summary>
    [SerializeField]
    public int numberOfElementsDisplayed = 6;
    private GameObject[] resourcePrerequisGameObject;
    private ResourcePrerequisite[] resourcePrerequisite;
    
    private PlayerResources playerResources;
    private RectTransform rectTransform;
    #endregion

    #region Initializer
    public GameObject Initialize(RectTransform parentRectTransform)
    {
        Transform myTransform = transform;
        this.rectTransform = GetComponent<RectTransform>();
        this.playerResources = ServiceContainer.Instance.GameObjectReferenceManager.Get("[PLAYER]").GetComponent<PlayerResources>();
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
    public void SetContent(ResourcePrerequisite[] content)
    {
        this.resourcePrerequisite = content;
        this.rectTransform.SetHeight(140.0f + this.grid.GetHeight(this.resourcePrerequisite.Length - 1 + this.grid.NumberOfElementsPerLine));
        
        this.EnableTheUsedResourcePrerequisite();

        ResourceHelper.SetResourcePrerequisiteUIGameObject(this.resourcePrerequisGameObject, this.resourcePrerequisite, this.playerResources);
    }

    private void EnableTheUsedResourcePrerequisite()
    {
        for (int resourcePrerequisiteIndex = 0; resourcePrerequisiteIndex < this.numberOfElementsDisplayed; resourcePrerequisiteIndex++)
            this.resourcePrerequisGameObject[resourcePrerequisiteIndex].SetActive(
                null == this.resourcePrerequisite ? false :
                resourcePrerequisiteIndex < this.resourcePrerequisite.Length);
    }
    #endregion
}