using UnityEngine;
using System.Collections;

public static class ColorHelper
{
    public static Color LightGreen = Create(118.0f, 240.0f, 157.0f);
    public static Color LightRed = Create(215.0f, 27.0f, 58.0f);

    public static Color Create(float red, float green, float blue, float alpha = 255.0f)
    {
        return new Color(red / 255.0f, green / 255.0f, blue / 255.0f, alpha / 255.0f);
    }
}
