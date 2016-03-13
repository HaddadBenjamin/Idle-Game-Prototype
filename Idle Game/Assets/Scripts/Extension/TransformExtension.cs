using UnityEngine;
using System.Collections;

public static class TransformExtension
{
    public static void ResetTransform(this Transform transform)
    {
        transform.position = Vector3.zero;
        transform.localRotation = Quaternion.identity;
        transform.localScale = new Vector3(1, 1, 1);
    }

    public static void FollowCusorPosition(this Transform transform)
    {
        Vector3 position = Input.mousePosition;

        position.z = transform.position.z - Camera.main.transform.position.z;
        transform.position = Camera.main.ScreenToWorldPoint(position);
    }
}
