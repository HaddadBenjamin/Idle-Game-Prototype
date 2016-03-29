using UnityEngine;

using System;
using System.Collections;
using System.Collections.Generic;

/// <summary>
/// Permet d'éviter de faire des news et des deletes sur des gameobjects en run-time.
/// Tous est configurable et cela permet de rendre le programme plus performant.
/// </summary>
public sealed class ObjectsPoolManager : AServiceComponent
{
    #region Fields
    /// <summary>
    /// Toute les pool de gameObject, ces dernières sont configurable à travers l'inspector.
    /// </summary>
    [SerializeField]
    private ObjectsPool[] objectsPools;
    /// <summary>
    /// Permet de récupérer les pool par une comparaison d'entier au lieu d'une comparaison de string : optimisation.
    /// </summary>
    private int[] objectsPoolsHashID;
    #endregion

    #region Initialize
    public override void InitializeByserviceContainer()
    {
        ObjectContainerHelper.InitializeHashIds(
            Array.ConvertAll(this.objectsPools, reference => reference.Prefab.name),
            ref this.objectsPoolsHashID);

        Array.ForEach(this.objectsPools, objectPool => objectPool.Initialize());
    }
    #endregion

    #region Behaviour Methods
    /// <summary>
    /// Permet de récupérer une pool de gameobject en fonction du nom du gameobject que l'on a besoin.
    /// </summary>
    /// <param name="poolName"></param>
    /// <returns></returns>
    public ObjectsPool GetPool(string poolName)
    {
        int hashID = ObjectContainerHelper.GetHashCodeIndex(poolName, ref this.objectsPoolsHashID);

        return this.objectsPools[hashID];
    }
    
    /// <summary>
    /// Desactive un objet de la pool correspondant à poolName.
    /// </summary>
    /// <param name="poolName"></param>
    /// <param name="gameObject"></param>
    public void RemoveObjectInPool(string poolName, GameObject gameObject)
    {
        if (null != gameObject)
            this.GetPool(poolName).RemoveObjectInPool(gameObject);
    }


    /// <summary>
    /// Desactive un objet de la pool correspondant à poolName après timeToWait secondes.
    /// </summary>
    /// <param name="poolName"></param>
    /// <param name="gameObject"></param>
    /// <param name="timeToWait"></param>
    public void RemoveObjectInPool(string poolName, GameObject gameObject, float timeToWait)
    {
        object[] parms = new object[3]{poolName, gameObject, timeToWait};

        StartCoroutine("RemoveObjectInPoolAfterNTime", parms);
    }

    /// <summary>
    /// Permet de désactiver un gameobject d'une pool après n temps.
    /// </summary>
    /// <param name="parms"></param>
    /// <returns></returns>
    private IEnumerator RemoveObjectInPoolAfterNTime(object[] parms)
    {
        yield return new WaitForSeconds((float)parms[2]);

        this.GetPool((string)parms[0]).RemoveObjectInPool((GameObject)parms[1]);
    }

    /// <summary>
    /// Rajoute un object dans une pool.
    /// </summary>
    /// <param name="poolName"></param>
    /// <returns></returns>
    public GameObject AddObjectInPool(string poolName)
    {
        return this.GetPool(poolName).AddObjectInPool();
    }

    /// <summary>
    /// Rajoute un object dans une pool et le positionne à la position objectPosition, avec la rotation objectRotation.
    /// </summary>
    /// <param name="poolName"></param>
    /// <param name="objectPosition"></param>
    /// <param name="objectRotation"></param>
    /// <returns></returns>
    public GameObject AddObjectInPool(string poolName, Vector3 objectPosition, Vector3 objectRotation)
    {
        GameObject gameObjectAdded = this.GetPool(poolName).AddObjectInPool();
        Transform gameObjectTransform = gameObjectAdded.transform;

        gameObjectTransform.position = objectPosition;
        gameObjectTransform.rotation = Quaternion.Euler(objectRotation);

        return gameObjectAdded;
    }

    /// <summary>
    /// Rajoute un object dans une pool et lui donne comme parent parentTransform.
    /// </summary>
    /// <param name="poolName"></param>
    /// <param name="parentTransform"></param>
    /// <returns></returns>
    public GameObject AddObjectInPool(string poolName, Transform parentTransform)
    {
        GameObject gameObjectAdded = this.AddObjectInPool(poolName);

        gameObjectAdded.transform.SetParent(parentTransform);

        return gameObjectAdded;
    }

    /// <summary>
    /// Rajoute un object dans une pool et le positionne à la position objectPosition, avec la rotation objectRotation et lui donne comme parent parentTransform.
    /// </summary>
    /// <param name="poolName"></param>
    /// <param name="objectPosition"></param>
    /// <param name="objectRotation"></param>
    /// <param name="parentTransform"></param>
    /// <returns></returns>
    public GameObject AddObjectInPool(string poolName, Vector3 objectPosition, Vector3 objectRotation, Transform parentTransform)
    {
        GameObject gameObjectAdded = this.AddObjectInPool(poolName, objectPosition, objectRotation);

        gameObjectAdded.transform.SetParent(parentTransform);

        return gameObjectAdded;
    }
    #endregion
}