using System;
using UnityEngine;

[System.Serializable]
public class SpriteReferences
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

public class SpriteReferencesArrays : MonoBehaviour
{
    #region Fields
    private SpriteReferences[] allStuffs;

    [SerializeField]
    private SpriteReferences resources;
    [SerializeField]
    private SpriteReferences resourcesBin;
    [SerializeField]
    private SpriteReferences raws;
    [SerializeField]
    private SpriteReferences rests;

    // Weapons
    [SerializeField]
    private SpriteReferences swords;
    [SerializeField]
    private SpriteReferences daggers;
    [SerializeField]
    private SpriteReferences axes;
    [SerializeField]
    private SpriteReferences spears;
    [SerializeField]
    private SpriteReferences maces;
    [SerializeField]
    private SpriteReferences staves;
    [SerializeField]
    private SpriteReferences bows;
    [SerializeField]
    private SpriteReferences guns;

    // Garments
    [SerializeField]
    private SpriteReferences armors;
    [SerializeField]
    private SpriteReferences clothes;
    [SerializeField]
    private SpriteReferences vests;
    [SerializeField]
    private SpriteReferences shields;
    [SerializeField]
    private SpriteReferences helmets;
    [SerializeField]
    private SpriteReferences hats;
    [SerializeField]
    private SpriteReferences gauntlets;
    [SerializeField]
    private SpriteReferences gloves;
    [SerializeField]
    private SpriteReferences shoes;
    [SerializeField]
    private SpriteReferences footwears;

    // Accessories
    [SerializeField]
    private SpriteReferences remedys;
    [SerializeField]
    private SpriteReferences spells;
    [SerializeField]
    private SpriteReferences potions;
    [SerializeField]
    private SpriteReferences projectiles;
    [SerializeField]
    private SpriteReferences rings;
    [SerializeField]
    private SpriteReferences musics;
    [SerializeField]
    private SpriteReferences pendants;
    #endregion

    #region Unity Methods
    void Awake()
    {
        this.allStuffs = new SpriteReferences[EnumHelper.Count<EStuffCategory>()];

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
        this.allStuffs[EnumHelper.GetIndex<EStuffCategory>(EStuffCategory.Ring)] = this.rings;
        this.allStuffs[EnumHelper.GetIndex<EStuffCategory>(EStuffCategory.Shield)] = this.shields;
        this.allStuffs[EnumHelper.GetIndex<EStuffCategory>(EStuffCategory.Shoes)] = this.shoes;
        this.allStuffs[EnumHelper.GetIndex<EStuffCategory>(EStuffCategory.Spear)] = this.spears;
        this.allStuffs[EnumHelper.GetIndex<EStuffCategory>(EStuffCategory.Spell)] = this.spells;
        this.allStuffs[EnumHelper.GetIndex<EStuffCategory>(EStuffCategory.Staff)] = this.staves;
        this.allStuffs[EnumHelper.GetIndex<EStuffCategory>(EStuffCategory.Sword)] = this.swords;
        this.allStuffs[EnumHelper.GetIndex<EStuffCategory>(EStuffCategory.Vest)] = this.vests;

        Array.ForEach(this.allStuffs, stuffsCategory => stuffsCategory.Initialize());

        this.resources.Initialize();
        this.resourcesBin.Initialize();
        this.raws.Initialize();
        this.rests.Initialize();
    }
    #endregion

    #region Behaviour Methods
    public Sprite Get(string stuffName, EStuffCategory stuffCategory)
    {
        return this.allStuffs[EnumHelper.GetIndex<EStuffCategory>(stuffCategory)].Get(stuffName);
    }

    public Sprite GetRawSprite(ERaw rawCategory)
    {
        return this.raws.Get(EnumHelper.GetRawCategoryString(rawCategory));
    }

    public Sprite GetResourceSprite(EResourceCategory resourceCategory)
    {
        return this.resources.Get(resourceCategory.ToString());
    }

    public Sprite GetResourceBinSprite(EResourceCategory resourceCategory)
    {
        return this.resourcesBin.Get(resourceCategory.ToString() + "Bin");
    }
    #endregion
}