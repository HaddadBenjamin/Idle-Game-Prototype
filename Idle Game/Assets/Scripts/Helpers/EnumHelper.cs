using System;

public static class EnumHelper
{
    /// <summary>
    /// Permet d'obtenir l'index d'une énumération.
    /// </summary>
    /// <typeparam name="EnumType"></typeparam>
    /// <param name="enumeration"></param>
    /// <returns></returns>
    public static int GetIndex<EnumType>(EnumType enumeration) where EnumType : struct, IConvertible
    {
        return Convert.ToInt32(enumeration);
    }

    /// <summary>
    /// Permet de savoir le nombre d'élements que contiend une énumeration.
    /// </summary>
    /// <typeparam name="EnumType"></typeparam>
    /// <returns></returns>
    public static int Count<EnumType>() where EnumType : struct, IConvertible
    {
        return System.Enum.GetValues(typeof(EnumType)).Length;
    }

    public static string GetRawCategoryString(ERaw rawCategory)
    {
        switch (rawCategory)
        {
            case ERaw.ShinyGem: return "Shiny Gem";
            case ERaw.ElvenDew: return "Elven Dew";
            case ERaw.ViperEssence: return "Viper Essence";
            case ERaw.IronWood: return "Iron Wood";
            case ERaw.BurningEmber: return "Burning Ember";
            case ERaw.RainbowDust: return "Rainbow Dust";
            case ERaw.MoonShard: return "Moon Shard";
            case ERaw.IronCarapace: return "Iron Carapace";
            case ERaw.WyvernWing: return "Wyvern Wing";
            case ERaw.FrozenCore: return "Frozen Core";
            case ERaw.RoyalBone: return "Royal Bone";
            case ERaw.LiquidFire: return "Liquid Fire";
            case ERaw.YggdrasilLeaf: return "Yggdrasil Leaf";
            case ERaw.SilverSteel: return "Silver Steel";
            case ERaw.PhoenixFeather: return "Phoenix Feather";
            case ERaw.GoldenThread: return "Golden Thread";
            case ERaw.DemonHeart: return "Demon Heart";
            case ERaw.DarkEnergy: return "Dark Energy";
            case ERaw.SunTear: return "Sun Tear";
            case ERaw.DragonScale: return "Dragon Scale";
            case ERaw.Adamantium: return "Adamantium";
            case ERaw.AncientEssence: return "Ancient Essence";
            case ERaw.FrostfireCrystal: return "Frostfire Crystal";
            case ERaw.ObsidianCoral: return "Obsidian Coral";
            case ERaw.ShardOfGaia: return "Shard Of Gaia";
            case ERaw.PrimalHorn: return "Primal Horn";
        }

        return null;
    }
}