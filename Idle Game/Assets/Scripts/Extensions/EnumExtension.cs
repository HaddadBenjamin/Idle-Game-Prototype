using System;

public static class EnumExtension
{
    public static int Count(this Enum enumeration)
    {
        return Enum.GetNames(enumeration.GetType()).Length;
    }

    public static int Count(this Type enumeration)
    {
        return Enum.GetNames(enumeration).Length;
    }
}
