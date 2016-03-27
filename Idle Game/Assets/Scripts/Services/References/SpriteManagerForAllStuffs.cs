using System;
using UnityEngine;

[System.Serializable]
public class SpriteManagerForAStuffCategory
{
    [SerializeField]
    protected Sprite[] references;
    protected int[] hashIds;

    public void Initialize()
    {
        ObjectContainerHelper.InitializeHashIds(
            Array.ConvertAll(this.references, reference => reference.name),
            ref this.hashIds);
    }

    public Sprite Get(string refenceName)
    {
        return this.references[ObjectContainerHelper.GetHashCodeIndex(refenceName, ref this.hashIds)];
    }
}

public class SpriteManagerForAllStuffs : MonoBehaviour
{
    #region Fields
    private SpriteManagerForAStuffCategory[] allStuffs;

    // Weapons
    [SerializeField]
    private SpriteManagerForAStuffCategory swords;
    [SerializeField]
    private SpriteManagerForAStuffCategory daggers;
    [SerializeField]
    private SpriteManagerForAStuffCategory axes;
    [SerializeField]
    private SpriteManagerForAStuffCategory spears;
    [SerializeField]
    private SpriteManagerForAStuffCategory maces;
    [SerializeField]
    private SpriteManagerForAStuffCategory staves;
    [SerializeField]
    private SpriteManagerForAStuffCategory bows;
    [SerializeField]
    private SpriteManagerForAStuffCategory guns;

    // Garments
    [SerializeField]
    private SpriteManagerForAStuffCategory armors;
    [SerializeField]
    private SpriteManagerForAStuffCategory clothes;
    [SerializeField]
    private SpriteManagerForAStuffCategory vests;
    [SerializeField]
    private SpriteManagerForAStuffCategory shields;
    [SerializeField]
    private SpriteManagerForAStuffCategory helmets;
    [SerializeField]
    private SpriteManagerForAStuffCategory hats;
    [SerializeField]
    private SpriteManagerForAStuffCategory gauntlets;
    [SerializeField]
    private SpriteManagerForAStuffCategory gloves;
    [SerializeField]
    private SpriteManagerForAStuffCategory shoes;
    [SerializeField]
    private SpriteManagerForAStuffCategory footwears;

    // Accessories
    [SerializeField]
    private SpriteManagerForAStuffCategory remedys;
    [SerializeField]
    private SpriteManagerForAStuffCategory spells;
    [SerializeField]
    private SpriteManagerForAStuffCategory potions;
    [SerializeField]
    private SpriteManagerForAStuffCategory projectiles;
    [SerializeField]
    private SpriteManagerForAStuffCategory rings;
    [SerializeField]
    private SpriteManagerForAStuffCategory musics;
    [SerializeField]
    private SpriteManagerForAStuffCategory pendants;
    [SerializeField]
    private SpriteManagerForAStuffCategory raws;
    #endregion

    #region Unity Methods
    void Awake()
    {
        this.allStuffs = new SpriteManagerForAStuffCategory[EnumHelper.Count<EStuffCategory>()];

        this.allStuffs[EnumHelper.GetIndex<EStuffCategory>(EStuffCategory.Armor)] = this.armors;
        this.allStuffs[EnumHelper.GetIndex<EStuffCategory>(EStuffCategory.Axe)] = this.axes;
        this.allStuffs[EnumHelper.GetIndex<EStuffCategory>(EStuffCategory.Bow)] = this.bows;
        this.allStuffs[EnumHelper.GetIndex<EStuffCategory>(EStuffCategory.Clothe)] = this.clothes;
        this.allStuffs[EnumHelper.GetIndex<EStuffCategory>(EStuffCategory.Dagger)] = this.daggers;
        this.allStuffs[EnumHelper.GetIndex<EStuffCategory>(EStuffCategory.Footwear)] = this.footwears;
        this.allStuffs[EnumHelper.GetIndex<EStuffCategory>(EStuffCategory.Gauntlet)] = this.gauntlets;
        this.allStuffs[EnumHelper.GetIndex<EStuffCategory>(EStuffCategory.Glove)] = this.gloves;
        this.allStuffs[EnumHelper.GetIndex<EStuffCategory>(EStuffCategory.Gun)] = this.guns;
        this.allStuffs[EnumHelper.GetIndex<EStuffCategory>(EStuffCategory.Hat)] = this.hats;
        this.allStuffs[EnumHelper.GetIndex<EStuffCategory>(EStuffCategory.Helmet)] = this.helmets;
        this.allStuffs[EnumHelper.GetIndex<EStuffCategory>(EStuffCategory.Mace)] = this.maces;
        this.allStuffs[EnumHelper.GetIndex<EStuffCategory>(EStuffCategory.Music)] = this.musics;
        this.allStuffs[EnumHelper.GetIndex<EStuffCategory>(EStuffCategory.Pendant)] = this.pendants;
        this.allStuffs[EnumHelper.GetIndex<EStuffCategory>(EStuffCategory.Potion)] = this.potions;
        this.allStuffs[EnumHelper.GetIndex<EStuffCategory>(EStuffCategory.Projectile)] = this.projectiles;
        this.allStuffs[EnumHelper.GetIndex<EStuffCategory>(EStuffCategory.Remedy)] = this.remedys;
        this.allStuffs[EnumHelper.GetIndex<EStuffCategory>(EStuffCategory.Resource)] = this.raws;
        this.allStuffs[EnumHelper.GetIndex<EStuffCategory>(EStuffCategory.Ring)] = this.rings;
        this.allStuffs[EnumHelper.GetIndex<EStuffCategory>(EStuffCategory.Shield)] = this.shields;
        this.allStuffs[EnumHelper.GetIndex<EStuffCategory>(EStuffCategory.Shoes)] = this.shoes;
        this.allStuffs[EnumHelper.GetIndex<EStuffCategory>(EStuffCategory.Spear)] = this.spears;
        this.allStuffs[EnumHelper.GetIndex<EStuffCategory>(EStuffCategory.Spell)] = this.spells;
        this.allStuffs[EnumHelper.GetIndex<EStuffCategory>(EStuffCategory.Staff)] = this.staves;
        this.allStuffs[EnumHelper.GetIndex<EStuffCategory>(EStuffCategory.Sword)] = this.swords;
        this.allStuffs[EnumHelper.GetIndex<EStuffCategory>(EStuffCategory.Vest)] = this.vests;

        Array.ForEach(this.allStuffs, stuffsCategory => stuffsCategory.Initialize());
    }
    #endregion

    #region Behaviour Methods
    public Sprite Get(string stuffName, EStuffCategory stuffCategory)
    {
        return this.allStuffs[EnumHelper.GetIndex<EStuffCategory>(stuffCategory)].Get(stuffName);
    }
    #endregion
}