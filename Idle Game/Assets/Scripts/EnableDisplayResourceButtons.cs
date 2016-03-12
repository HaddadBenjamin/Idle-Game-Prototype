using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class EnableDisplayResourceButtons : MonoBehaviour
{
    void Awake()
    {
        GetComponent<Button>().onClick.AddListener(() =>
        {
            GameObject.FindGameObjectWithTag("ServiceLocator").GetComponent<ServiceLocator>().GameObjectReferenceManager.Get("Button Building Container").SetActive(true);
            //base.EnableResourceAnimationMenu();
        });
    }
}
