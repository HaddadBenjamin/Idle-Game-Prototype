using UnityEngine;

public static class ColorExtension
{
    public static void A(this Color color, float alpha)
    {
        color = new Color(color.r, color.g, color.b, alpha);
    }
}