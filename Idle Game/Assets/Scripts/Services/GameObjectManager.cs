using UnityEngine;
using System.Collections;
using System;

public class GameObjectManager : AReferenceContainer<GameObject>
{
    public GameObject Instantiate(string gameobjectName)
    {
        return GameObject.Instantiate(this.Get(gameobjectName));
    }

    public GameObject Instantiate(string gameobjectName, Vector3 position, Vector3 eulerAngles)
    {
        return GameObject.Instantiate(this.Get(gameobjectName), position, Quaternion.Euler(eulerAngles)) as GameObject;
    }
}


