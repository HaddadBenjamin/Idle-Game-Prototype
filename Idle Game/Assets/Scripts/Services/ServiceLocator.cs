using UnityEngine;
using System.Collections;

public class ServiceLocator : MonoBehaviour
{
    #region Attributes & Properties
    public MaterialReferences MaterialManager { get; private set; }
    public StuffsConfiguration StuffsConfiguration { get; private set; }
    public SpriteReferencesArrays SpriteReferencesArrays { get; private set; }
    public GameObjectReferencesArrays GameObjectReferencesArrays { get; private set; }
    public GameObjectReferenceManager GameObjectReferenceManager { get; private set; }
    public BuildingsConfiguration BuildingsConfiguration { get; private set; }
    public EventManager<EEvent> EventManager { get; private set; }
    public EventManagerParamsInt<EResourceCategory> EventManagerResourceGenerated { get; private set; }
    public EventManagerParamsInt<EResourceCategory> EventManagerResourceNumberHaveBeenUpdated { get; private set; }
    public EventManagerParamsConstructionSquareArrayAndInt<EEventParamsConstructionSquareArrayAndInt> EventManagerParamsConstructionSquareArrayAndInt { get; private set; }
    public ObjectsPoolManager ObjectsPoolManager { get; private set; }

    private static ServiceLocator instance = null;
    
    public static ServiceLocator Instance
    {
        get
        {
            if (null == instance)
            {
                instance = GameObject.FindGameObjectWithTag("ServiceLocator").GetComponent<ServiceLocator>();

                instance.Initialize();

                DontDestroyOnLoad(instance);
            }

            return instance;
        }

        private set
        { 
            instance = value; 
        }
    }
    #endregion

    #region Initialisation
    private void Initialize()
    {
        this.BuildingsConfiguration = gameObject.GetComponent<BuildingsConfiguration>();
        this.StuffsConfiguration = gameObject.GetComponent<StuffsConfiguration>();

        this.EventManagerParamsConstructionSquareArrayAndInt = new EventManagerParamsConstructionSquareArrayAndInt<EEventParamsConstructionSquareArrayAndInt>();
        this.EventManagerResourceNumberHaveBeenUpdated = new EventManagerParamsInt<EResourceCategory>();
        this.EventManagerResourceGenerated = new EventManagerParamsInt<EResourceCategory>();
        this.EventManager = new EventManager<EEvent>();

        this.SpriteReferencesArrays = gameObject.GetComponent<SpriteReferencesArrays>();
        this.GameObjectReferencesArrays = gameObject.GetComponent<GameObjectReferencesArrays>();

        AServiceComponent[] servicesComponent =
        {
            (this.ObjectsPoolManager = gameObject.GetComponent<ObjectsPoolManager>()),
            (this.MaterialManager = gameObject.GetComponent<MaterialReferences>()),
            (this.GameObjectReferenceManager = gameObject.GetComponent<GameObjectReferenceManager>()),
        };

        foreach (AServiceComponent serviceComponent in servicesComponent)
            serviceComponent.InitializeByServiceLocator();

        //GameObject[] pool = new GameObject[150];

        //for (int i = 0; i < 150; i++)
        //    pool[i] = this.ObjectsPoolManager.AddObjectInPool("Stone Mine");

        //this.ObjectsPoolManager.RemoveObjectInPool("Stone Mine", pool[3]);
        //this.ObjectsPoolManager.RemoveObjectInPool("Stone Mine", pool[1], 3.0f);
        ////this.ObjectsPoolManager.AddObjectInPool("Stone Mine");
        ////this.ObjectsPoolManager.AddObjectInPool("Stone Mine");
    }
    #endregion
}