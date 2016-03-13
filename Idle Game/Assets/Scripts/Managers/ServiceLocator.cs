using UnityEngine;
using System.Collections;

/// <summary>
/// Abstract Service Locator, contains all the services and creates the services that are the same for each platform.
/// </summary>
public class ServiceLocator : MonoBehaviour
{
    #region Attributes & Properties
    public TextureManager TextureManager { get; protected set; }
    public MaterialManager MaterialManager { get; protected set; }
    public SpriteManager SpriteManager { get; protected set; }
    public GameObjectManager GameObjectManager { get; protected set; }
    public GameObjectReferenceManager GameObjectReferenceManager { get; protected set; }
    #endregion

    void Awake()
    {
        Initialize(gameObject);
    }

    #region Initialisation
    public void Initialize(GameObject gameObject)
    {
        CommuneInitializationForEachPlatforms(gameObject);
    }

    private void CommuneInitializationForEachPlatforms(GameObject gameObject)
    {
        AServiceComponent[] servicesComponent =
        {
            (TextureManager = gameObject.GetComponent<TextureManager>()),
            (SpriteManager = gameObject.GetComponent<SpriteManager>()),
            (MaterialManager = gameObject.GetComponent<MaterialManager>()),
            (GameObjectManager = gameObject.GetComponent<GameObjectManager>()),
            (GameObjectReferenceManager = gameObject.GetComponent<GameObjectReferenceManager>()),
        };

        foreach (AServiceComponent serviceComponent in servicesComponent)
            serviceComponent.InitializeByServiceLocator();
    }
    #endregion
}