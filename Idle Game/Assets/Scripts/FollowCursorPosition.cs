using UnityEngine;
using System.Collections;

public class FollowCursorPosition : MonoBehaviour
{
    Transform myTransform;

    void Awake()
    {
        this.myTransform = transform;
    }

	void Update ()
    {
        this.myTransform.FollowCusorPosition();
	}
}
