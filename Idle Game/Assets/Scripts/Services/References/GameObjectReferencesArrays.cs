using System;
using UnityEngine;

public class GameObjectReferencesArrays : AServiceComponent
{
    #region Fields
    private GameObjectReferences[] gameObjects;
    [SerializeField]
    private GameObjectReferences resourceBuildings;
    [SerializeField]
    private GameObjectReferences UI;
    [SerializeField]
    private GameObjectReferences rest;
    #endregion

    #region Unity Methods
    public override void InitializeByserviceContainer()
    {
        this.gameObjects = new GameObjectReferences[EnumHelper.Count<EGameObjectReferences>()];
        this.gameObjects[EnumHelper.GetIndex<EGameObjectReferences>(EGameObjectReferences.ResourceBuildings)]   = this.resourceBuildings;
        this.gameObjects[EnumHelper.GetIndex<EGameObjectReferences>(EGameObjectReferences.UI)]                  = this.UI;
        this.gameObjects[EnumHelper.GetIndex<EGameObjectReferences>(EGameObjectReferences.Rest)]                = this.rest;

        Array.ForEach(this.gameObjects, go => go.Initialize());
    }
    #endregion

    #region Behaviour Methods
    public GameObject Get(string gameobjectName, EGameObjectReferences referenceCategory)
    {
        return this.gameObjects[EnumHelper.GetIndex<EGameObjectReferences>(referenceCategory)].Get(gameobjectName);
    }

    public GameObject Instantiate(string gameobjectName, EGameObjectReferences referenceCategory)
    {
        return this.gameObjects[EnumHelper.GetIndex<EGameObjectReferences>(referenceCategory)].Instantiate(gameobjectName);
    }

    public GameObject Instantiate(string gameobjectName, Vector3 position, Vector3 eulerAngles, EGameObjectReferences referenceCategory)
    {
        return this.gameObjects[EnumHelper.GetIndex<EGameObjectReferences>(referenceCategory)].Instantiate(gameobjectName, position, eulerAngles);
    }

    public GameObject Instantiate(string gameobjectName, Vector3 position, Vector3 eulerAngles, Transform parent, EGameObjectReferences referenceCategory)
    {
        return this.gameObjects[EnumHelper.GetIndex<EGameObjectReferences>(referenceCategory)].Instantiate(gameobjectName, position, eulerAngles, parent);
    }

    public GameObject Instantiate(string gameobjectName, Vector3 position, Vector3 rotation, Vector3 scale, Transform parent, EGameObjectReferences referenceCategory)
    {
        return this.gameObjects[EnumHelper.GetIndex<EGameObjectReferences>(referenceCategory)].Instantiate(gameobjectName, position, rotation, scale, parent);
    }

    public GameObject Instantiate(string gameobjectName, Transform parent, EGameObjectReferences referenceCategory)
    {
        return this.gameObjects[EnumHelper.GetIndex<EGameObjectReferences>(referenceCategory)].Instantiate(gameobjectName, parent);
    }
    #endregion
}