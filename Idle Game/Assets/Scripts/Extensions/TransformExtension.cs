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

    public static void SetPositionAndRotationAndParent(this Transform transform, Vector3 position, Vector3 rotation, Transform parent)
    {
        transform.SetParent(parent);

        transform.localPosition = position;
        transform.localEulerAngles = rotation;
    }

    public static void SetPositionX(this Transform transform, float positionX)
    {
        transform.position = new Vector3(positionX, transform.position.y, transform.position.z);
    }

    public static void SetPositionY(this Transform transform, float positionY)
    {
        transform.position = new Vector3(transform.position.x, positionY, transform.position.z);
    }

    public static void SetPositionZ(this Transform transform, float positionZ)
    {
        transform.position = new Vector3(transform.position.x, transform.position.y, positionZ);
    }

    public static void SetLocalPositionX(this Transform transform, float positionX)
    {
        transform.localPosition = new Vector3(positionX, transform.localPosition.y, transform.localPosition.z);
    }

    public static void SetLocalPositionY(this Transform transform, float positionY)
    {
        transform.localPosition = new Vector3(transform.localPosition.x, positionY, transform.localPosition.z);
    }

    public static void SetLocalPositionZ(this Transform transform, float positionZ)
    {
        transform.localPosition = new Vector3(transform.localPosition.x, transform.localPosition.y, positionZ);
    }

    public static void SetRotationX(this Transform transform, float rotationX)
    {
        transform.eulerAngles = new Vector3(rotationX, transform.eulerAngles.y, transform.eulerAngles.z);
    }

    public static void SetRotationY(this Transform transform, float rotationY)
    {
        transform.eulerAngles = new Vector3(transform.eulerAngles.x, rotationY, transform.eulerAngles.z);
    }

    public static void SetRotationZ(this Transform transform, float rotationZ)
    {
        transform.eulerAngles = new Vector3(transform.eulerAngles.x, transform.eulerAngles.y, rotationZ);
    }

    public static void SetLocalRotationX(this Transform transform, float rotationX)
    {
        transform.localEulerAngles = new Vector3(rotationX, transform.localEulerAngles.y, transform.localEulerAngles.z);
    }

    public static void SetLocalRotationY(this Transform transform, float rotationY)
    {
        transform.localEulerAngles = new Vector3(transform.localEulerAngles.x, rotationY, transform.localEulerAngles.z);
    }

    public static void SetLocalRotationZ(this Transform transform, float rotationZ)
    {
        transform.localEulerAngles = new Vector3(transform.localEulerAngles.x, transform.localEulerAngles.y, rotationZ);
    }

    public static void SetScaleX(this Transform transform, float scaleX)
    {
        transform.localScale = new Vector3(scaleX, transform.localScale.y, transform.localScale.z);
    }

    public static void SetScaleY(this Transform transform, float scaleY)
    {
        transform.localScale = new Vector3(transform.localScale.x, scaleY, transform.localScale.z);
    }

    public static void SetScaleZ(this Transform transform, float scaleZ)
    {
        transform.localScale = new Vector3(transform.localScale.x, transform.localScale.y, scaleZ);
    }

    public static void FollowCursorPosition(this Transform transform, float distanceFromMainCamera)
    {
        Vector3 position = Input.mousePosition;
        position.z = distanceFromMainCamera;
        transform.position = Camera.main.ScreenToWorldPoint(position);
    }

    public static void FollowCursorPositionWithDefinedHeight(this Transform transform, float definedHeight)
    {
        Vector3 position = Input.mousePosition;
       // position.y = definedHeight;
        transform.position = Camera.main.ScreenToWorldPoint(position);
        //Vector3 transformPosition = transform.position;
        //transform.position = new Vector3(transformPosition.x, definedHeight, transformPosition.z);
    }
}
