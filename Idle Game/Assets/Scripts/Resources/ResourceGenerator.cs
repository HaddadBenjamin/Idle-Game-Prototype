//Le joueur contiend une classe de data permettant de savoir combien il génère de chaque type de resources
//Le bùatiment contiend une classe de données pour savoir combien iil génère de resources
// La vitesse de la génération de resources est local au joueur, c'est que le joueur qui génère ses resources
// A l'ajout et la suppresion d'un bâtiment le montant de resource généré diminue

//[System.Serializable]
//public class ResourceLevel
//{
//    public Action<int> LevelUpDelegate; //Synergy + building upgrade production + modification of level//eventManager;
//    private ResourcePrerequisite priceToLevelUp;
//}

//public class PlayerResource
//{
//    private Alarm resourceGeneratorTimer;
//    private ResourceGeneratorConfiguration[] resourceGenerator;
//}


//Lors du placement d'un bâtiment on augmente les resourceGenerated ?
//// pareil pour la destruction

///// <summary>
///// Le timer est dans la classe playerResource de sorte à généré toutes les resources au même moment ?
///// </summary>
//public class ResourceGenerator : MonoBehaviour
//{
//    #region Fields
//    [SerializeField]
//    private float resourcedGeneratePerSeconds;
//    private float numberOfResourceGenerated;
//    private float totalOfResourceGenerated;
//    private Alarm timer;
//    [SerializeField]
//    private EResourceCategory resourceTypeGenerated;
//    private ResourceLevel resourceLevel;
//    private string buildingName;
//    public Action<float> ResourceHaveBeenGeneratedDelegate;
//    #endregion

//    #region Constructor
//    public ResourceGenerator(string buildingName, EResourceCategory resourceType)
//    {
//        //this.resourceLevel = ServiceLocator.Instance.BuildingsConfiguration.GetConfiguration(buildingName).GetLevel(0);
//    }
//    #endregion
//    #region Properties
//    #endregion

//    #region Behaviour Methods
//    /// <summary>
//    /// Appeler lors d'un ajout de bâtiment ou d'un level up ?
//    /// </summary>
//    /// <param name="resourceGeneratedAdded"></param>
//    public void AddResourceGeneratedPerTimer(float resourceGeneratedAdded)
//    {
//        this.resourcedGeneratePerSeconds += resourceGeneratedAdded;
//    }

//    /// <summary>
//    /// Appeler lors d'une suppresion de bâtiment ?
//    /// </summary>
//    /// <param name="resourceGeneratedSubstract"></param>
//    public void SubstractResourceGeneratedPerTimer(float resourceGeneratedSubstract)
//    {
//        this.resourcedGeneratePerSeconds -= resourceGeneratedSubstract;
//    }

//    public void Generate()
//    {
//        this.numberOfResourceGenerated += this.resourcedGeneratePerSeconds;
//        this.totalOfResourceGenerated += this.resourcedGeneratePerSeconds;

//        if (this.numberOfResourceGenerated >= 1.0f)
//        {
//            int resourceGeneratedAsInt = Mathf.CeilToInt(this.numberOfResourceGenerated);

//            this.ResourceHaveBeenGeneratedDelegate(resourceGeneratedAsInt);
//            //playerResource.AddResource(resourceCategory, numberOfResourceGenerated.Ceil());

//            this.numberOfResourceGenerated -= Mathf.Ceil(this.numberOfResourceGenerated);
//        }	
//    }
//    #endregion
//}
//J'ai ecris des choses dans mon cahier sur l'architecture des resources

//Génération de resources :
//Génère un type de resource
//génère tous les n temps ? (Alarm ?) : temps fixe : 1.0f seconde
//génère n resource tous les n temps : n resorceGenerated
//niveau qui augmente le nombre de resource généré
//des syngergy ? en fonction du niveau débloqué
//génère idle ?

//float numberOfResourceGenerated;
//Alarm alarm;
//Level level; : buildingConfiguration

//constructor()
//{
//    level = ServiceLocator.Instance.GetConfiguration(buildingName).GetLevel(0);
//}

//void Generate()
//{
//    if (alarm.RingUpdated())
//        numberOfResourceGenerated += resourceGenerated;

//    if (numberOfResourceGenerated >= 1.0f)
//    {
//        playerResource.AddResource(resourceCategory, numberOfResourceGenerated.Ceil());
//        numberOfResourceGenerated -= numberOfResourceGenerated.Ceil();
//    }	
//}

//if (leftClickOnButton(level.isNotMaximumLevel())
//{
//    level.LevelUp();
//}
