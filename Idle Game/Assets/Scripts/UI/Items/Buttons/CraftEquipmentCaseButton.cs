using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class CraftEquipmentCaseButton : AMenuAnimationButton
{
    #region Fields
    private ECraftEquipmentMode craftEquipmentMode;
    private Alarm alarm;
  
    // Les références
    private Transform myTransform;
    private Image equipmentImage;
    private Image craftImage;
    private Image progressBarCraftingEquipment;
    private Text text;

    private StuffConfiguration stuffConfiguration;

    private PlayerStuffs playerStuffs;
    private PlayerResources playerResources;
    private PlayerRaws playerRaws;
    #endregion

    #region Properties
    public ECraftEquipmentMode CraftEquipmentMode
    {
        get { return craftEquipmentMode; }
        private set { craftEquipmentMode = value; }
    }
    #endregion

    #region Unity Methods
    void Awake()
    {
        this.craftEquipmentMode = ECraftEquipmentMode.Waiting;

        this.myTransform = transform;

        this.text = this.myTransform.Find("Text").gameObject.GetComponent<Text>();
        this.equipmentImage = this.myTransform.Find("Equipment Image").gameObject.GetComponent<Image>();
        this.craftImage = this.myTransform.Find("Craft Image").gameObject.GetComponent<Image>();
        this.progressBarCraftingEquipment = this.myTransform.Find("Progress Bar").gameObject.GetComponent<Image>();

        this.playerStuffs = ServiceContainer.Instance.GameObjectReferenceManager.Get("[PLAYER]").GetComponent<PlayerStuffs>();
        this.playerResources = ServiceContainer.Instance.GameObjectReferenceManager.Get("[PLAYER]").GetComponent<PlayerResources>();
        this.playerRaws = ServiceContainer.Instance.GameObjectReferenceManager.Get("[PLAYER]").GetComponent<PlayerRaws>();
    }

    void Start()
    {
        base.BaseStart();

        base.Button.onClick.AddListener(() =>
        {
            if (ECraftEquipmentMode.Waiting == this.craftEquipmentMode)
                base.MenusAnimations.OpenCraftEquipmentMenu();
            else if (ECraftEquipmentMode.Collect == this.craftEquipmentMode)
                this.Collect();
            else
                ServiceContainer.Instance.TextInformationManager.AddTextInformation("You have to wait, your " + this.stuffConfiguration.StuffCategory + " is not finish to be crafted");
        });
    }
    #endregion

    #region Unity Methods
    void Update()
    {
        if (null != this.alarm)
        {
            System.TimeSpan timespan = System.TimeSpan.FromSeconds(this.alarm.GetTimeToWait());

            this.text.text = 
                timespan.Hours > 0      ? string.Format("{0}h {1:D2}m", timespan.Hours, timespan.Minutes) :
                timespan.Minutes > 0    ? string.Format("{0}m {1:D2}s", timespan.Minutes, timespan.Seconds) :
                                          string.Format("{0}s",timespan.Seconds);

            this.progressBarCraftingEquipment.fillAmount = this.alarm.Ratio();

            if (this.alarm.IsRingingUpdated())
            {
                this.craftEquipmentMode = ECraftEquipmentMode.Collect;
                this.text.text = "Collect";

                this.alarm = null;
            }
        }
    }
    #endregion

    #region Behaviour Methods
    public bool CanAddItemToCollect()
    {
        return this.craftEquipmentMode == ECraftEquipmentMode.Waiting;

    }
    public void AddItemToColect(StuffConfiguration stuffConfiguration)
    {
        // Collect pour avoir le commportement qui marche mais pas fini
        this.craftEquipmentMode = ECraftEquipmentMode.Crafting;

        this.stuffConfiguration = stuffConfiguration;

        this.craftImage.enabled = false;
        this.equipmentImage.enabled = true;

        this.equipmentImage.sprite = ServiceContainer.Instance.SpriteReferencesArrays.Get(stuffConfiguration.StuffName, stuffConfiguration.StuffCategory);

        this.alarm = new Alarm(stuffConfiguration.TimeToCraft, false);
    }

    public void Collect()
    {
        this.craftEquipmentMode = ECraftEquipmentMode.Waiting;

         if (!this.playerResources.HaveEnoughtResource(this.stuffConfiguration.ResourcesPrerequisite))
                ServiceContainer.Instance.TextInformationManager.AddTextInformation("You dont have enough resources to craft this " + this.stuffConfiguration.StuffCategory, ETextInformation.Warning);
         else
         {
            if (!this.playerRaws.HaveEnoughtRaw(this.stuffConfiguration.RawsPrerequisite))
                ServiceContainer.Instance.TextInformationManager.AddTextInformation("You dont have enough raws to craft this " + this.stuffConfiguration.StuffCategory, ETextInformation.Warning);
            else
            {
                if (!this.playerStuffs.HaveEnoughtStuff(this.stuffConfiguration.StuffsPrerequisite))
                    ServiceContainer.Instance.TextInformationManager.AddTextInformation("You dont have enough stuffs to craft this " + this.stuffConfiguration.StuffCategory, ETextInformation.Warning);
                else
                {
                    this.playerResources.Pay(this.stuffConfiguration.ResourcesPrerequisite);
                    this.playerRaws.Pay(this.stuffConfiguration.RawsPrerequisite);
                    this.playerStuffs.Pay(this.stuffConfiguration.StuffsPrerequisite);

                    EStuffQuality quality = StuffHelper.GenerateStuffQuality();
               
                    // L'équipement broadsword de type épée et de qualité common have been created
                    ServiceContainer.Instance.TextInformationManager.AddTextInformation(
                        "The stuff " + this.stuffConfiguration.StuffName + 
                        " of type " + this.stuffConfiguration.StuffCategory + 
                        " and quality " + quality +
                        " haveebeenCreated");

                    this.playerStuffs.AddStuff(this.stuffConfiguration.StuffName, this.stuffConfiguration.StuffCategory, quality, 1);
                }
            }
         }

         this.text.text = "Craft!";
         this.craftImage.enabled = true;
         this.equipmentImage.enabled = false;

         this.equipmentImage.sprite = ServiceContainer.Instance.SpriteReferencesArrays.Get(stuffConfiguration.StuffName, stuffConfiguration.StuffCategory);
    }
    #endregion
}
