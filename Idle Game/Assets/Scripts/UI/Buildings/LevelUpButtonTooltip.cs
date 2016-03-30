using UnityEngine;

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

    #region Unity Methods
    void Awake()
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
    }
    #endregion

    #region Behaviour Methods
    public void SetContent(ResourcePrerequisite[] content)
    {
        // désabonné si il l'était
        this.resourcePrerequisite = content;

        this.EnableTheUsedResourcePrerequisite();

        this.rectTransform.SetHeight(100.0f + this.grid.GetHeight(this.resourcePrerequisite.Length - 1));
        //Tableau de resourcPrerequisiteGameObject, resourcePrerequisite content
        //Tableau de resourcPrerequisiteGameObject, resourcePrerequisite content, differenceFromPlayerResource
        // Les abonné à un resource changer ?
        // Set Text via un helper (resourcePrerequisiteHelper ?)
    }

    private void EnableTheUsedResourcePrerequisite()
    {
        for (int resourcePrerequisiteIndex = 0; resourcePrerequisiteIndex < this.numberOfElementsDisplayed; resourcePrerequisiteIndex++)
            this.resourcePrerequisGameObject[resourcePrerequisiteIndex].SetActive(resourcePrerequisiteIndex < this.resourcePrerequisite.Length);
    }
    #endregion
}