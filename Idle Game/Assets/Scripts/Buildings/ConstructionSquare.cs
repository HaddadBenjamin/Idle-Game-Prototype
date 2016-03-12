using UnityEngine;
using System.Collections;

public class ConstructionSquare : MonoBehaviour
{
    private Material beginMaterial = null;
    private ServiceLocator serviceLocator = null;
    private bool thereIsAlreadySomethingHere = false;

	void Awake ()
    {
        this.beginMaterial = GetComponent<Renderer>().material;
	}

    void Start()
    {
        this.serviceLocator = GameObject.FindGameObjectWithTag("ServiceLocator").GetComponent<ServiceLocator>();
    }

    void OnMouseOver()
    {
        GetComponent<Renderer>().material = thereIsAlreadySomethingHere ?
                                            serviceLocator.MaterialManager.Get("RedOutline") :
                                            serviceLocator.MaterialManager.Get("GreenOutline");
    }

    void OnMouseExit()
    {
        GetComponent<Renderer>().material = this.beginMaterial;
    }
}
