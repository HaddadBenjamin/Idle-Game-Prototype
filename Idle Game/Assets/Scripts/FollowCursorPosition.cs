using UnityEngine;
using System.Collections;

public class FollowCursorPosition : MonoBehaviour
{
    private Transform myTransform;

    void Awake()
    {
        this.myTransform = transform;
    }

	void Update ()
    {
        this.myTransform.FollowCursorPosition(10.0f);
	}
}
