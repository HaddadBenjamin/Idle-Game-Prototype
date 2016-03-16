using UnityEngine;
using System.Collections;

public class ConstructionSquare : MonoBehaviour
{
    private Material beginMaterial = null;
    private ServiceLocator serviceLocator = null;
    private bool thereIsABuildingHere = false;
    private bool showOutline = false;
    private int cellVertical;
    private int cellHorizontal;

    public int CellVertical
    {
        get { return cellVertical; }
        set { cellVertical = value; }
    }

    public int CellHorizontal
    {
        get { return cellHorizontal; }
        set { cellHorizontal = value; }
    }

    public bool ThereIsABuildingHere
    {
        get { return thereIsABuildingHere; }
        set { thereIsABuildingHere = value; }
    }

    public bool ShowOutline
    {
        get { return showOutline; }
        set
        { 
            showOutline = value;

            if (true == showOutline)
            {
                GetComponent<Renderer>().material = this.thereIsABuildingHere ?
                                                    this.serviceLocator.MaterialManager.Get("RedOutline") :
                                                    this.serviceLocator.MaterialManager.Get("GreenOutline");
            }
            else
                GetComponent<Renderer>().material = this.beginMaterial;
        
        }
    }

	void Awake ()
    {
        this.beginMaterial = GetComponent<Renderer>().material;
	}

    void Start()
    {
        this.serviceLocator = GameObject.FindGameObjectWithTag("ServiceLocator").GetComponent<ServiceLocator>();
    }
}
