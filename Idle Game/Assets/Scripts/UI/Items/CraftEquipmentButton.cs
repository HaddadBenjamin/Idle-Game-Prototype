using UnityEngine;
using UnityEngine.UI;

public class CraftEquipmentButton : MonoBehaviour
{
    #region Fields
    private ResourcePrerequisite[] resourcesPrerequisite;
    private RawPrerequisite[] rawsPrerequisite;
    private StuffPrerequisite[] stuffsPrerequisite;

    private PlayerResources playerResources;
    private PlayerRaws playerRaws;
    private PlayerStuffs playerStuffs;

    private StuffConfiguration stuffConfiguration;
    #endregion

    #region Initializer
    public void Initialize(ResourcePrerequisite[] resources, RawPrerequisite[] raws, StuffPrerequisite[] stuffs, StuffConfiguration stuffConfiguration)
    {
        this.resourcesPrerequisite = resources;
        this.rawsPrerequisite = raws;
        this.stuffsPrerequisite = stuffs;

        this.stuffConfiguration = stuffConfiguration;

        GameObject player = ServiceLocator.Instance.GameObjectReferenceManager.Get("[PLAYER]");

        this.playerResources = player.GetComponent<PlayerResources>();
        this.playerRaws = player.GetComponent<PlayerRaws>();
        this.playerStuffs = player.GetComponent<PlayerStuffs>();

        GetComponent<Button>().onClick.AddListener(() =>
        {
            Debug.LogFormat("can pay : res  {0}, raws {1}, stuffs {2}:",
                this.playerResources.HaveEnoughtResource(this.resourcesPrerequisite),
                this.playerRaws.HaveEnoughtRaw(this.rawsPrerequisite),
                this.playerStuffs.HaveEnoughtStuff(this.stuffsPrerequisite));

            if (this.playerResources.HaveEnoughtResource(this.resourcesPrerequisite) &&
                this.playerRaws.HaveEnoughtRaw(this.rawsPrerequisite) &&
                this.playerStuffs.HaveEnoughtStuff(this.stuffsPrerequisite))
            {
                this.playerResources.Pay(this.resourcesPrerequisite);
                this.playerRaws.Pay(this.rawsPrerequisite);
                this.playerStuffs.Pay(this.stuffsPrerequisite);

                // Méthodes pou générer la qualité ?
                this.playerStuffs.AddStuff(this.stuffConfiguration.StuffName, this.stuffConfiguration.StuffCategory, this.GenerateStuffQuality(), 1);
                // Add Stuf
            }
        });
    }
    #endregion

    #region Behaviour Methods
    private EStuffQuality GenerateStuffQuality()
    {
        int randomNumber = MathHelper.GenerateRandomBeetweenTwoInts(1, 2500);
        
        return      randomNumber <= 2000 ? EStuffQuality.Common :
                    randomNumber <= 2200 ? EStuffQuality.Good :
                    randomNumber <= 2350 ? EStuffQuality.Great :
                    randomNumber <= 2415 ? EStuffQuality.Flawless :
                    randomNumber <= 2445 ? EStuffQuality.Epic :
                    randomNumber <= 2480 ? EStuffQuality.Legendary :
                                           EStuffQuality.Mythical;
    }
    #endregion
}