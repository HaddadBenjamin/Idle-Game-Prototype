using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class AResourceMenu : MonoBehaviour
{
    protected Button button;
    protected PlayerMenu playerMenu;
    protected Animator animator;
    
    void Awake()
    {
        this.button = GetComponent<Button>();
        this.playerMenu = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerMenu>();
        this.animator = GameObject.Find("Canvas").GetComponent<Animator>();
    }

    protected void OpenConstructionMenu()
    {
        this.DisableMenus();
        this.animator.SetBool("constructionMenu", true);

        this.playerMenu.CurrentMenu = EPlayerMenu.Construction;
    }

    protected void OpenResourceConstructionMenu()
    {
        this.DisableMenus();
        this.animator.SetBool("resourceConstructionMenu", true);

        // On a pas accès à la méthode SetActive dans l'animator, d'où la raison de ce cette ligne de code sale.
        GameObject.FindGameObjectWithTag("ServiceLocator").GetComponent<ServiceLocator>().GameObjectReferenceManager.Get("Button Building Container").SetActive(true);
       
        this.playerMenu.CurrentMenu = EPlayerMenu.ResourceConstruction;
    }

    private void DisableMenus()
    {
        this.animator.SetBool("defaultMenu", false);
        this.animator.SetBool("constructionMenu", false);
        this.animator.SetBool("resourceConstructionMenu", false);
    }
}
