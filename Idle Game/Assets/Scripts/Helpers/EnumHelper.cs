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
}