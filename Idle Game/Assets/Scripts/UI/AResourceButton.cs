using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class AResourceMenu : MonoBehaviour
{
    protected Button button;
    protected PlayerMenuAnimation playerMenuAnimation;
    protected Animator animator;
    protected GameObject buttonBuildingContainerGameObject;
    protected CanvasGroup canvasGroup;
    protected ServiceLocator serviceLocator;
    protected ScrollRect buildingContainerScrollRectMask;
    protected RTSCamera RTSCamera;
    
    protected void BaseStart()
    {
        this.button = GetComponent<Button>();
        this.playerMenuAnimation = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerMenuAnimation>();
        this.canvasGroup = GameObject.Find("Canvas").GetComponent<CanvasGroup>();
        this.animator = GameObject.Find("Canvas").GetComponent<Animator>();
        this.serviceLocator = ServiceLocator.Instance;
        this.RTSCamera = Camera.main.GetComponent<RTSCamera>();

        this.buttonBuildingContainerGameObject = this.serviceLocator.GameObjectReferenceManager.Get("Button Building Container");
        this.buildingContainerScrollRectMask = this.serviceLocator.GameObjectReferenceManager.Get("Mask Button Building Container").GetComponent<ScrollRect>();
    }

    protected void OpenConstructionMenu()
    {
        this.DisableCanvasInteractionWhenAnimationOccur("OpenConstructionMenu", 1.0f);

        this.DisableMenus();
        this.animator.SetBool("constructionMenu", true);
        this.RTSCamera.enabled = false;

        this.playerMenuAnimation.CurrentMenuAnimation = EPlayerMenuAnimation.Construction;
    }

    protected void OpenResourceConstructionMenu()
    {
        this.DisableCanvasInteractionWhenAnimationOccur("OpenResourceConstructionMenu", 1.0f);

        this.DisableMenus();
        this.animator.SetBool("resourceConstructionMenu", true);

        // On a pas accès à la méthode SetActive dans l'animator, d'où la raison de ce cette ligne de code sale.
        this.buttonBuildingContainerGameObject.SetActive(true);
        this.buildingContainerScrollRectMask.horizontalNormalizedPosition = 0.0f;

        this.playerMenuAnimation.CurrentMenuAnimation = EPlayerMenuAnimation.ResourceConstruction;
    }

    protected void CloseConstructionMenu()
    {
        this.DisableCanvasInteractionWhenAnimationOccur("OpenConstructionMenu", -1.0f);

        this.DisableMenus();
        this.animator.SetBool("defaultMenu", true);
        this.RTSCamera.enabled = true;

        // On a pas accès à la méthode SetActive dans l'animator, d'où la raison de ce cette ligne de code sale.
        this.buttonBuildingContainerGameObject.SetActive(false);

        this.playerMenuAnimation.CurrentMenuAnimation = EPlayerMenuAnimation.Construction;
    }

    protected void CloseResourceConstructionMenu()
    {
        this.DisableCanvasInteractionWhenAnimationOccur("OpenResourceConstructionMenu", -1.0f);

        this.DisableMenus();
        this.animator.SetBool("constructionMenu", true);

        // On a pas accès à la méthode SetActive dans l'animator, d'où la raison de ce cette ligne de code sale.
        this.buttonBuildingContainerGameObject.SetActive(false);

        this.playerMenuAnimation.CurrentMenuAnimation = EPlayerMenuAnimation.Construction;
    }

    private void DisableMenus()
    {
        this.animator.SetBool("defaultMenu", false);
        this.animator.SetBool("constructionMenu", false);
        this.animator.SetBool("resourceConstructionMenu", false);
    }

    // Se lance normalement avec StartCoroutine ou pas forcément ?
    // Désactiver les intéractions sur le canvas actuel pendant que le temps de l'anmation "currentAnimationName" se joue et défini la vitesse que l'animation joue.
    private IEnumerator DisableCanvasInteractionWhenAnimationOccur(string currentAnimationName, float animationSpeedMultiplicator)
    {
        this.animator.SetFloat("animationSpeedMultiplicator", animationSpeedMultiplicator);

        float animationLength = Mathf.Abs(this.playerMenuAnimation.GetAnimation(currentAnimationName).length * animationSpeedMultiplicator);

        //yield return new WaitForEndOfFrame();
        this.canvasGroup.interactable = false;
        yield return new WaitForSeconds(animationLength);
        this.canvasGroup.interactable = true;
    }
}
