using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class MenusAnimations : MonoBehaviour
{
    #region Fields
    [SerializeField]
    private AnimationClip[] animations;
    public EMenuAnimation CurrentMenuAnimation { get; private set; }

    private ServiceLocator serviceLocator;
    private CanvasGroup canvasGroup;
    private Animator animator;

    private GameObject buttonBuildingContainerGameObject;
    private ScrollRect buildingContainerScrollRectMask;
    private RTSCamera RTSCamera;
    private GenerateCraftEquipmentButtons generateCraftEquipmentButtons;
    #endregion

    #region Constructor
    public MenusAnimations()
    {
        this.CurrentMenuAnimation = EMenuAnimation.Default;
    }
    #endregion

    #region Properties
    public AnimationClip GetAnimation(string animationName)
    {
        foreach (AnimationClip animation in animations)
        {
            if (animationName == animation.name)
                return animation;
        }

        return null;
    }
    #endregion

    #region Unity Methods
    void Start()
    {
        this.canvasGroup = GetComponent<CanvasGroup>();
        this.animator = GetComponent<Animator>();
        this.serviceLocator = ServiceLocator.Instance;

        this.serviceLocator.EventManager.SubcribeToEvent(EEvent.ClickOnBuilding, this.OpenBuildingInteractionsMenu);

        this.RTSCamera = Camera.main.GetComponent<RTSCamera>();

        this.buttonBuildingContainerGameObject = this.serviceLocator.GameObjectReferenceManager.Get("Button Building Container");
        this.buildingContainerScrollRectMask = this.serviceLocator.GameObjectReferenceManager.Get("Mask Button Building Container").GetComponent<ScrollRect>();
        this.generateCraftEquipmentButtons = ServiceLocator.Instance.GameObjectReferenceManager.Get("Craft Equipment Button Container").GetComponent<GenerateCraftEquipmentButtons>();
    }
    #endregion

    #region Behaviour Methods
    protected void DisableMenus()
    {
        this.animator.SetBool("defaultMenu", false);
        this.animator.SetBool("constructionMenu", false);
        this.animator.SetBool("resourceConstructionMenu", false);
        this.animator.SetBool("buildingInteractionsMenu", false);
        this.animator.SetBool("craftEquipmentMenu", false);
        this.animator.SetBool("craftEquipmenCategoryMenu", false);
    }

    // Désactiver les intéractions sur le canvas actuel pendant que le temps de l'anmation "currentAnimationName" se joue et défini la vitesse que l'animation joue.
    public IEnumerator DisableCanvasInteractionWhenAnimationOccur(string currentAnimationName, float animationSpeedMultiplicator)
    {
        this.animator.SetFloat("animationSpeedMultiplicator", animationSpeedMultiplicator);

        float animationLength = Mathf.Abs(this.GetAnimation(currentAnimationName).length * animationSpeedMultiplicator);

        //yield return new WaitForEndOfFrame();
        this.canvasGroup.interactable = false;
        yield return new WaitForSeconds(animationLength);
        this.canvasGroup.interactable = true;
    }

    public void OpenConstructionMenu()
    {
        this.CurrentMenuAnimation = EMenuAnimation.Construction;

        this.DisableCanvasInteractionWhenAnimationOccur("OpenConstructionMenu", 1.0f);

        this.DisableMenus();
        this.animator.SetBool("constructionMenu", true);
    }

    public void OpenResourceConstructionMenu()
    {
        this.CurrentMenuAnimation = EMenuAnimation.ResourceConstruction;

        this.DisableCanvasInteractionWhenAnimationOccur("OpenResourceConstructionMenu", 1.0f);

        this.DisableMenus();
        this.animator.SetBool("resourceConstructionMenu", true);

        // On a pas accès à la méthode SetActive dans l'animator, d'où la raison de ce cette ligne de code sale.
        this.buttonBuildingContainerGameObject.SetActive(true);
        this.buildingContainerScrollRectMask.horizontalNormalizedPosition = 0.0f;
    }

    public void CloseConstructionMenu()
    {
        this.CurrentMenuAnimation = EMenuAnimation.Default;

        this.DisableCanvasInteractionWhenAnimationOccur("OpenConstructionMenu", -1.0f);

        this.DisableMenus();
        this.animator.SetBool("defaultMenu", true);
        this.RTSCamera.enabled = true;

        // On a pas accès à la méthode SetActive dans l'animator, d'où la raison de ce cette ligne de code sale.
        this.buttonBuildingContainerGameObject.SetActive(false);
    }

    public void CloseResourceConstructionMenu()
    {
        this.CurrentMenuAnimation = EMenuAnimation.Construction;

        this.DisableCanvasInteractionWhenAnimationOccur("OpenResourceConstructionMenu", -1.0f);

        this.DisableMenus();
        this.animator.SetBool("constructionMenu", true);

        // On a pas accès à la méthode SetActive dans l'animator, d'où la raison de ce cette ligne de code sale.
        this.buttonBuildingContainerGameObject.SetActive(false);
    }

    private void OpenBuildingInteractionsMenu()
    {
        this.CurrentMenuAnimation = EMenuAnimation.BuildingInteractions;

        this.DisableCanvasInteractionWhenAnimationOccur("openBuildingInteractionsMenu", 1.0f);

        this.DisableMenus();

        this.animator.SetBool("buildingInteractionsMenu", true);
    }

    public void CloseBuildingInteractionsMenu()
    {
        this.CurrentMenuAnimation = EMenuAnimation.Default;
      
        this.DisableCanvasInteractionWhenAnimationOccur("openBuildingInteractionsMenu", -1.0f);

        this.DisableMenus();

        this.animator.SetBool("defaultMenu", true);
    }

    public void OpenCraftEquipmentMenu()
    {
        this.CurrentMenuAnimation = EMenuAnimation.CraftEquipment;

        this.DisableCanvasInteractionWhenAnimationOccur("openCraftEquipmentMenu", 1.0f);

        this.DisableMenus();

        this.animator.SetBool("craftEquipmentMenu", true);
    }

    public void CloseCraftEquipmentMenu()
    {
        this.CurrentMenuAnimation = EMenuAnimation.Default;

        this.DisableCanvasInteractionWhenAnimationOccur("openCraftEquipmentMenu", -1.0f);

        this.DisableMenus();

        this.animator.SetBool("defaultMenu", true);
    }

    public void OpenCraftEquipmentCategoryMenu(EStuffCategory craftEquipmentCategoryToOpen)
    {
        this.CurrentMenuAnimation = EMenuAnimation.CraftEquipmentCategory;

        this.DisableCanvasInteractionWhenAnimationOccur("openCraftEquipmentCategoryMenu", 1.0f);

        this.DisableMenus();

        this.animator.SetBool("craftEquipmenCategoryMenu", true);

        this.generateCraftEquipmentButtons.EnableCraftCategoryEquipmentButtonsContainer(craftEquipmentCategoryToOpen);
    }

    public void CloseCraftEquipmentCategoryMenu()
    {
        this.CurrentMenuAnimation = EMenuAnimation.CraftEquipment;

        this.DisableCanvasInteractionWhenAnimationOccur("openCraftEquipmentCategoryMenu", -1.0f);

        this.DisableMenus();

        this.animator.SetBool("craftEquipmentMenu", true);

        this.generateCraftEquipmentButtons.DisableCraftCategoriesEquipmentButtonsContainer();
    }
    #endregion
}
