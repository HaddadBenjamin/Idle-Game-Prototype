using UnityEngine;
using System.Collections;

public static class ColorHelper
{
    public static Color LightGreen = Create(118.0f, 240.0f, 157.0f);
    public static Color Green = Create(59.0f, 224.0f, 56.0f);
    public static Color Blue = Create(59.0f, 87.0f, 221.0f);
    public static Color Yellow = Create(246.0f, 230.0f, 13.0f);
    public static Color Purple = Create(233.0f, 13.0f, 246.0f);
    public static Color Red = Create(246.0f, 13.0f, 46.0f);
    public static Color LightRed = Create(215.0f, 27.0f, 58.0f);

    public static Color Create(float red, float green, float blue, float alpha = 255.0f)
    {
        return new Color(red / 255.0f, green / 255.0f, blue / 255.0f, alpha / 255.0f);
    }
}
