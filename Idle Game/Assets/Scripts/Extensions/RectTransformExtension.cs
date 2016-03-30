using UnityEngine;
using System.Collections;

public static class RectTransformExtension
{
    public static void SetPosition(this RectTransform rectTransform, Vector3 position)
    {
        rectTransform.localPosition = position;
    }

    public static void SetRotation(this RectTransform rectTransform, Vector3 rotation)
    {
        rectTransform.localRotation = Quaternion.Euler(rotation);
    }

    public static void SetScale(this RectTransform rectTransform, Vector3 scale)
    {
        rectTransform.localScale = scale;
    }

    public static void SetWidth(this RectTransform rectTransform, float width)
    {
        rectTransform.sizeDelta = new Vector2(width, rectTransform.sizeDelta.y);
    }

    public static void SetHeight(this RectTransform rectTransform, float height)
    {
        rectTransform.sizeDelta = new Vector2(rectTransform.sizeDelta.x, height);
    }

    public static float GetWidth(this RectTransform rectTransform)
    {
        return rectTransform.sizeDelta.x;
    }

    public static float GetHeight(this RectTransform rectTransform)
    {
        return rectTransform.sizeDelta.y;
    }

    public static float GetOffsetPivotX(this RectTransform rectTransform)
    {
        return -(rectTransform.pivot.x * rectTransform.GetWidth());
    }

    public static float GetOffsetPivotY(this RectTransform rectTransform)
    {
        return -(rectTransform.pivot.y * rectTransform.GetHeight());
    }
}