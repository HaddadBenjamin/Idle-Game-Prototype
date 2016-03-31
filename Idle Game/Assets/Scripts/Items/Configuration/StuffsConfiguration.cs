using UnityEngine;
using System;

public class StuffsConfiguration : MonoBehaviour
{

    #region Fields
    private StuffConfiguration[][] allStuffs;

    // Weapons
    [SerializeField]
    private StuffConfiguration[] swords;
    [SerializeField]
    private StuffConfiguration[] daggers;
    [SerializeField]
    private StuffConfiguration[] axes;
    [SerializeField]
    private StuffConfiguration[] spears;
    [SerializeField]
    private StuffConfiguration[] maces;
    [SerializeField]
    private StuffConfiguration[] staves;
    [SerializeField]
    private StuffConfiguration[] bows;
    [SerializeField]
    private StuffConfiguration[] guns;

    // Garments
    [SerializeField]
    private StuffConfiguration[] armors;
    [SerializeField]
    private StuffConfiguration[] clothes;
    [SerializeField]
    private StuffConfiguration[] vests;
    [SerializeField]
    private StuffConfiguration[] shields;
    [SerializeField]
    private StuffConfiguration[] helmets;
    [SerializeField]
    private StuffConfiguration[] hats;
    [SerializeField]
    private StuffConfiguration[] gauntlets;
    [SerializeField]
    private StuffConfiguration[] gloves;
    [SerializeField]
    private StuffConfiguration[] shoes;
    [SerializeField]
    private StuffConfiguration[] footwears;

    // Accessories
    [SerializeField]
    private StuffConfiguration[] remedys;
    [SerializeField]
    private StuffConfiguration[] spells;
    [SerializeField]
    private StuffConfiguration[] potions;
    [SerializeField]
    private StuffConfiguration[] projectiles;
    [SerializeField]
    private StuffConfiguration[] rings;
    [SerializeField]
    private StuffConfiguration[] musics;
    [SerializeField]
    private StuffConfiguration[] pendants;
    #endregion

    #region Properties
    #endregion

    #region Unity Methods
    void Awake()
    {
        this.allStuffs = new StuffConfiguration[EnumHelper.Count<EStuffCategory>()][];

        this.allStuffs[EnumHelper.GetIndex<EStuffCategory>(EStuffCategory.Armor)]           = this.armors;
        this.allStuffs[EnumHelper.GetIndex<EStuffCategory>(EStuffCategory.Axe)]             = this.axes;
        this.allStuffs[EnumHelper.GetIndex<EStuffCategory>(EStuffCategory.Bow)]             = this.bows;
        this.allStuffs[EnumHelper.GetIndex<EStuffCategory>(EStuffCategory.Clothe)]          = this.clothes;
        this.allStuffs[EnumHelper.GetIndex<EStuffCategory>(EStuffCategory.Dagger)]          = this.daggers;
        this.allStuffs[EnumHelper.GetIndex<EStuffCategory>(EStuffCategory.Footwear)]        = this.footwears;
        this.allStuffs[EnumHelper.GetIndex<EStuffCategory>(EStuffCategory.Gauntlet)]        = this.gauntlets;
        this.allStuffs[EnumHelper.GetIndex<EStuffCategory>(EStuffCategory.Glove)]           = this.gloves;
        this.allStuffs[EnumHelper.GetIndex<EStuffCategory>(EStuffCategory.Gun)]             = this.guns;
        this.allStuffs[EnumHelper.GetIndex<EStuffCategory>(EStuffCategory.Hat)]             = this.hats;
        this.allStuffs[EnumHelper.GetIndex<EStuffCategory>(EStuffCategory.Helmet)]          = this.helmets;
        this.allStuffs[EnumHelper.GetIndex<EStuffCategory>(EStuffCategory.Mace)]            = this.maces;
        this.allStuffs[EnumHelper.GetIndex<EStuffCategory>(EStuffCategory.Music)]           = this.musics;
        this.allStuffs[EnumHelper.GetIndex<EStuffCategory>(EStuffCategory.Pendant)]         = this.pendants;
        this.allStuffs[EnumHelper.GetIndex<EStuffCategory>(EStuffCategory.Potion)]          = this.potions;
        this.allStuffs[EnumHelper.GetIndex<EStuffCategory>(EStuffCategory.Projectile)]      = this.projectiles;
        this.allStuffs[EnumHelper.GetIndex<EStuffCategory>(EStuffCategory.Remedy)]          = this.remedys;
        this.allStuffs[EnumHelper.GetIndex<EStuffCategory>(EStuffCategory.Ring)]            = this.rings;
        this.allStuffs[EnumHelper.GetIndex<EStuffCategory>(EStuffCategory.Shield)]          = this.shields;
        this.allStuffs[EnumHelper.GetIndex<EStuffCategory>(EStuffCategory.Shoes)]           = this.shoes;
        this.allStuffs[EnumHelper.GetIndex<EStuffCategory>(EStuffCategory.Spear)]           = this.spears;
        this.allStuffs[EnumHelper.GetIndex<EStuffCategory>(EStuffCategory.Spell)]           = this.spells;
        this.allStuffs[EnumHelper.GetIndex<EStuffCategory>(EStuffCategory.Staff)]           = this.staves;
        this.allStuffs[EnumHelper.GetIndex<EStuffCategory>(EStuffCategory.Sword)]           = this.swords;
        this.allStuffs[EnumHelper.GetIndex<EStuffCategory>(EStuffCategory.Vest)]            = this.vests;

        for (int stuffCategoryIndex = 0; stuffCategoryIndex < this.allStuffs.Length; stuffCategoryIndex++)
            Array.ForEach(this.allStuffs[stuffCategoryIndex], stuff => stuff.InitializeStuffCategory((EStuffCategory)stuffCategoryIndex));
    }
    #endregion

    #region Behaviour Methods
    public StuffConfiguration GetStuffConfiguration(string stuffName, EStuffCategory stuffCategory)
    {
        return Array.Find(this.allStuffs[EnumHelper.GetIndex<EStuffCategory>(stuffCategory)], stuff => stuff.StuffName == stuffName);
    }

    public StuffConfiguration[] GetStuffsConfiguration(EStuffCategory stuffCategory)
    {
        return this.allStuffs[EnumHelper.GetIndex<EStuffCategory>(stuffCategory)];
    }
    #endregion
}