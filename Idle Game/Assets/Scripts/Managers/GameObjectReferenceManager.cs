using UnityEngine;
using System.Collections;
using System;

public class GameObjectReferenceManager : AServiceComponent
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

    public GameObject Get(string GameObjectName)
    {
        return this.gameObjects[ObjectContainerHelper.GetHashCodeIndex(GameObjectName, ref hashIds)];
    }
}


