using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class AResourceMenu : MonoBehaviour
{
    protected Button button;
    
    void Awake()
    {
        this.button = GetComponent<Button>();
    }

    protected void EnableResourceAnimationMenu()
    {
        GameObject.Find("Canvas").GetComponent<Animator>().SetBool("enableResourceMenu", true);
    }

    void OnMouseDrag()
    {

    }
}
