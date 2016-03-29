using UnityEngine;
using System.Collections;

public class ServiceContainer : MonoBehaviour
{
    #region Attributes & Properties
    public MaterialReferences MaterialReferences { get; private set; }

    public GameObjectReferencesArrays GameObjectReferencesArrays { get; private set; }
    public SpriteReferencesArrays SpriteReferencesArrays { get; private set; }

    public ObjectsPoolManager ObjectsPoolManager { get; private set; }
    public GameObjectReferenceManager GameObjectReferenceManager { get; private set; }

    public StuffsConfiguration StuffsConfiguration { get; private set; }
    public BuildingsConfiguration BuildingsConfiguration { get; private set; }

    public EventManager<EEvent> EventManager { get; private set; }
    public EventManagerParamsInt<ERaw> EventManagerRawNumberHaveBeenUpdated { get; private set; }
    public EventManagerParamsInt<EResourceCategory> EventManagerResourceGenerated { get; private set; }
    public EventManagerParamsInt<EResourceCategory> EventManagerResourceNumberHaveBeenUpdated { get; private set; }
    public EventManagerParamsVector3<EEventParamsVector3> EventManagerParamsVector3 { get; private set; }
    public EventManagerDoubleEnumParamsIntAndString<EStuffCategory, EStuffQuality> EventManagerStuffNumberHaveBeenUpdated { get; private set; }
    public EventManagerParamsConstructionSquareArrayAndInt<EEventParamsConstructionSquareArrayAndInt> EventManagerParamsConstructionSquareArrayAndInt { get; private set; }

    public TextInformationManager TextInformationManager { get; private set; }

    private static ServiceContainer instance = null;
    
    public static ServiceContainer Instance
    {
        get
        {
            if (null == instance)
            {
                instance = GameObject.FindGameObjectWithTag("ServiceContainer").GetComponent<ServiceContainer>();

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

        this.EventManagerParamsVector3 = new EventManagerParamsVector3<EEventParamsVector3>();
        this.EventManagerParamsConstructionSquareArrayAndInt = new EventManagerParamsConstructionSquareArrayAndInt<EEventParamsConstructionSquareArrayAndInt>();
        this.EventManagerResourceNumberHaveBeenUpdated = new EventManagerParamsInt<EResourceCategory>();
        this.EventManagerStuffNumberHaveBeenUpdated = new EventManagerDoubleEnumParamsIntAndString<EStuffCategory, EStuffQuality>();
        this.EventManagerRawNumberHaveBeenUpdated = new EventManagerParamsInt<ERaw>();
        this.EventManagerResourceGenerated = new EventManagerParamsInt<EResourceCategory>();
        this.EventManager = new EventManager<EEvent>();

        AServiceComponent[] servicesComponent =
        {
            (this.ObjectsPoolManager = gameObject.GetComponent<ObjectsPoolManager>()),
            (this.MaterialReferences = gameObject.GetComponent<MaterialReferences>()),
            (this.GameObjectReferenceManager = gameObject.GetComponent<GameObjectReferenceManager>()),
            (this.GameObjectReferencesArrays = gameObject.GetComponent<GameObjectReferencesArrays>()),
            (this.TextInformationManager = gameObject.GetComponent<TextInformationManager>()),
            (this.SpriteReferencesArrays = gameObject.GetComponent<SpriteReferencesArrays>()),
        };

        foreach (AServiceComponent serviceComponent in servicesComponent)
            serviceComponent.InitializeByserviceContainer();

        this.EventManager.CallEvent(EEvent.ServicesHaveBeenInitialized);
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