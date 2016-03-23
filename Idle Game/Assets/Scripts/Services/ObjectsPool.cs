using System;
using System.Collections.Generic;
using System.Collections;

using UnityEngine;

/// <summary>
/// Contient une pool de gameobject configurable via l'interface d'Unity.
/// </summary>
[System.Serializable]
public class ObjectsPool
{
    #region Fields
    /// <summary>
    /// Si la pool est étendable on utilisera ici une liste, parcontre c'est moins rapide qu'un tableau.
    /// </summary>
    private List<GameObject> extandablePool;
    /// <summary>
    /// Si la pool n'est pas étendable on peut alors utiliser un tableau et profiter d'optimisation intéressantes.
    /// </summary>
    private GameObject[] notExtandablePool;
    /// <summary>
    /// Correspond à la prefab qui sera dans la pool.
    /// </summary>
    [SerializeField]
    private GameObject prefab;
    /// <summary>
    /// Taille de base de cette pool, dans le cas ou la pool n'est pas extendable ce nombre ne peut pas changer.
    /// </summary>
    [SerializeField]
    private int initializationSize;
    /// <summary>
    /// Détermine si la taille de la pool est fixe ou non.
    /// </summary>
    [SerializeField]
    private bool extandable;

    /// <summary>
    /// Index de la pool qui est mit à jour dans les méthodes de comportement.
    /// </summary>
    private int poolIndex = 0;
    #endregion

    #region Properties
    public GameObject Prefab
    {
        get { return prefab; }
        private set { prefab = value; }
    }

    public int InitializationSize
    {
        get { return initializationSize; }
        private set { initializationSize = value; }
    }

    public bool Extandable
    {
        get { return extandable; }
        private set { extandable = value; }
    }
    #endregion

    #region Initialize
    /// <summary>
    /// Crée les gameobject de la pool dans la bonne pool puis les désactive de sorte à pouvoir les reactiver lorsque l'on aura besoin et donc eviter de faire des new / Destroy par GameObject.Instantiate / Destroy.
    /// </summary>
    public void Initialize()
    {
        if (this.Extandable)
        {
            this.extandablePool = new List<GameObject>();

            for (int objectToInstantiatedIndex = 0; objectToInstantiatedIndex < this.InitializationSize; objectToInstantiatedIndex++)
            {
                GameObject newGameObject = GameObject.Instantiate(this.Prefab);

                newGameObject.SetActive(false);

                this.extandablePool.Add(newGameObject);
            }
        }
        else
        {
            this.notExtandablePool = new GameObject[this.InitializationSize];

            for (int objectToInstantiatedIndex = 0; objectToInstantiatedIndex < this.InitializationSize; objectToInstantiatedIndex++)
            {
                this.notExtandablePool[objectToInstantiatedIndex] = GameObject.Instantiate(this.Prefab);

                this.notExtandablePool[objectToInstantiatedIndex].SetActive(false);
            }
        }
    }
    #endregion

    #region Behaviour Methods
    /// <summary>
    /// Récupère l'objet à index donné puis le désactive.
    /// </summary>
    /// <param name="gameObject"></param>
    public void RemoveObjectInPool(GameObject gameObject)
    {
        this.SetAndGetPoolIndexAtGameObjectIndex(gameObject);

        gameObject.SetActive(false);
    }

    /// <summary>
    /// Rajoute un objet dans la pool en fonction de plusieurs critères détaillé plus bas.
    /// </summary>
    /// <returns></returns>
    public GameObject AddObjectInPool()
    {
        GameObject gameObjectAtIndex = this.GetGameObjectAtPoolIndex();

        // Si l'objet est à l'index poolIndex.
        if (null != gameObjectAtIndex)
        {
            this.UpdatePoolIndex();

            gameObjectAtIndex.SetActive(true);

            return gameObjectAtIndex;
        }
        else
        {
            GameObject firstDisableGameObject = this.GetTheFirstDisableGameObject();

            // Si il y a un objet désactiver dans la pool.
            if (null != firstDisableGameObject)
            {
                this.poolIndex = this.SetAndGetPoolIndexAtGameObjectIndex(firstDisableGameObject) + 1;

                firstDisableGameObject.SetActive(true);

                return firstDisableGameObject;
            }
            else
            {
                // Sinon soit on aggrandit la pool et on update cette index, soit on met à jour l'index et renvoi l'objet à cette index.

                if (this.Extandable)
                {
                    GameObject newGameObject = GameObject.Instantiate(this.prefab);
                    this.extandablePool.Add(newGameObject);
                    ++this.initializationSize;
                    this.UpdatePoolIndex();

                    return newGameObject;
                }
                else
                {
                    this.UpdatePoolIndex();

                    return this.GetGameObjectAtPoolIndex();
                }
            }
        }
    }

    /// <summary>
    /// Détermine si il est possible de mettre l'index de la pool à 0.
    /// </summary>
    /// <returns></returns>
    private bool CanResetThePoolIndex()
    {
        return this.poolIndex >= this.initializationSize &&
            !this.Extandable;
    }

    /// <summary>
    ///  Incrémente l'index de la pool.
    /// </summary>
    private void IncrementPoolIndex()
    {
        ++this.poolIndex;
    }

    /// <summary>
    /// Récupère l'index de pool d'un gameobject et modifie la valeur de poolIndex.
    /// </summary>
    /// <param name="gameObject"></param>
    /// <returns></returns>
    private int SetAndGetPoolIndexAtGameObjectIndex(GameObject gameObject)
    {
        if (this.Extandable)
        {
            for (int gameObjectIndex = 0; gameObjectIndex < this.extandablePool.Count; gameObjectIndex++)
            {
                if (this.extandablePool[gameObjectIndex] == gameObject)
                    this.poolIndex = gameObjectIndex;
            }
        }
        else
        {
            for (int gameObjectIndex = 0; gameObjectIndex < this.notExtandablePool.Length; gameObjectIndex++)
            {
                if (this.notExtandablePool[gameObjectIndex] == gameObject)
                    this.poolIndex = gameObjectIndex;
            }
        }

        this.ResetPoolIndexIfPossible();

        return this.poolIndex;
    }

    /// <summary>
    ///  Incrèmente l'index de la pool puis la remet à 0 si elle est égal ou supérieur à la taille de la pool et que la pool n'est pas étendable.
    /// </summary>
    private void UpdatePoolIndex()
    {
        this.IncrementPoolIndex();

        this.ResetPoolIndexIfPossible();
    }

    /// <summary>
    /// Remet la taille de la pool à 0 si cela est pertinent et possible.
    /// </summary>
    private void ResetPoolIndexIfPossible()
    {
        if (this.CanResetThePoolIndex())
            this.poolIndex = 0;
    }

    /// <summary>
    /// Remet la taille de la pool à 0.
    /// </summary>
    private void ResetPoolIndex()
    {
        this.poolIndex = 0;
    }

    /// <summary>
    /// Récupère l'objet à l'index pool index.
    /// </summary>
    /// <returns></returns>
    private GameObject GetGameObjectAtPoolIndex()
    {
        return this.poolIndex >= this.initializationSize ? 
                null :
                    this.Extandable ?
                    this.extandablePool[this.poolIndex] :
                    this.notExtandablePool[this.poolIndex];
    }

    /// <summary>
    /// Fait une recherche sur la pool et renvoi le première objet étant désactivé.
    /// </summary>
    /// <returns></returns>
    private GameObject GetTheFirstDisableGameObject()
    {
        return (this.Extandable) ?
                this.extandablePool.Find(gameObject => !gameObject.activeSelf) :
                Array.Find(this.notExtandablePool, gameObject => !gameObject.activeSelf);
    }
    #endregion
}