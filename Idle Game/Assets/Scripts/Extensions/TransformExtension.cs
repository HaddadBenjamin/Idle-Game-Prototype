using UnityEngine;
using System.Collections;

public static class TransformExtension
{
    public static void ResetTransform(this Transform transform)
    {
        transform.position = Camera.main.transform.position;
        transform.localRotation = Quaternion.identity;
        transform.localScale = new Vector3(1, 1, 1);
    }

    public static void FollowCursorPosition(this Transform transform, float distanceFromMainCamera)
    {
        Vector3 position = Input.mousePosition;
        position.z = distanceFromMainCamera;
        transform.position = Camera.main.ScreenToWorldPoint(position);
    }
}
