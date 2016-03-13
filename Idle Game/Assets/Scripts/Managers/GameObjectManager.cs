using UnityEngine;
using System.Collections;
using System;

public class GameObjectManager : AServiceComponent
{
    [SerializeField]
    private GameObject[] gameObjects;
    private int[] hashIds;

    public override void InitializeByServiceLocator()
    {
        ObjectContainerHelper.InitializeHashIds(
            Array.ConvertAll(this.gameObjects, gameObject => gameObject.name), 
            ref this.hashIds);
    }

    public GameObject Get(string gameobjectName)
    {
        return this.gameObjects[ObjectContainerHelper.GetHashCodeIndex(gameobjectName, ref hashIds)];
    }

    public GameObject Instantiate(string gameobjectName)
    {
        return GameObject.Instantiate(this.Get(gameobjectName));
    }

    public GameObject Instantiate(string gameobjectName, Vector3 position, Vector3 eulerAngles)
    {
        return GameObject.Instantiate(this.Get(gameobjectName), position, Quaternion.Euler(eulerAngles)) as GameObject;
    }
}


