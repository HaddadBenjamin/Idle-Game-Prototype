using System;
using UnityEngine;

public enum EGameObjectReferences
{
    ResourceBuildings,
    UI,
}

[System.Serializable]
public class GameObjectReferences
{
    #region Fields
    [SerializeField]
    protected GameObject[] references;
    protected int[] hashIds;
    #endregion

    #region Initializer
    public void Initialize()
    {
        ObjectContainerHelper.InitializeHashIds(
            Array.ConvertAll(this.references, reference => reference.name),
            ref this.hashIds);
    }
    #endregion

    #region Behaviour Methods
    public GameObject Get(string refenceName)
    {
        return this.references[ObjectContainerHelper.GetHashCodeIndex(refenceName, ref this.hashIds)];
    }

    public GameObject Instantiate(string gameobjectName)
    {
        return GameObject.Instantiate(this.Get(gameobjectName)) as GameObject;
    }

    public GameObject Instantiate(string gameobjectName, Vector3 position, Vector3 eulerAngles)
    {
        return GameObject.Instantiate(this.Get(gameobjectName), position, Quaternion.Euler(eulerAngles)) as GameObject;
    }

    public GameObject Instantiate(string gameobjectName, Vector3 position, Vector3 eulerAngler, Transform parent)
    {
        GameObject newObject = this.Instantiate(gameobjectName, parent);

        newObject.transform.localPosition = position;
        newObject.transform.eulerAngles = eulerAngler;

        return newObject;
    }

    public GameObject Instantiate(string gameobjectName, Vector3 position, Vector3 rotation, Vector3 scale, Transform parent)
    {
        GameObject newObject = this.Instantiate(gameobjectName, position, rotation, parent);

        newObject.transform.localScale = scale;

        return newObject;
    }

    public GameObject Instantiate(string gameobjectName, Transform parent)
    {
        GameObject newObject = this.Instantiate(gameobjectName);

        newObject.transform.SetParent(parent);

        return newObject;
    }
    #endregion
}

public class GameObjectReferencesArrays : AServiceComponent
{
    #region Fields
    [SerializeField]
    private GameObjectReferences resourceBuildings;
    [SerializeField]
    private GameObjectReferences UI;
    #endregion

    #region Unity Methods
    public override void InitializeByserviceContainer()
    {
        this.resourceBuildings.Initialize();
        this.UI.Initialize();
    }
    #endregion

    #region Behaviour Methods
    public GameObject Get(string gameobjectName, EGameObjectReferences referenceCategory)
    {
        switch (referenceCategory)
        {
            case EGameObjectReferences.ResourceBuildings: return this.resourceBuildings.Get(gameobjectName);
            case EGameObjectReferences.UI: return this.UI.Get(gameobjectName);
        }

        return null;
    }

    public GameObject Instantiate(string gameobjectName, EGameObjectReferences referenceCategory)
    {
        switch (referenceCategory)
        {
            case EGameObjectReferences.ResourceBuildings: return this.resourceBuildings.Instantiate(gameobjectName);
            case EGameObjectReferences.UI: return this.UI.Instantiate(gameobjectName);
        }

        return null; 
    }

    public GameObject Instantiate(string gameobjectName, Vector3 position, Vector3 eulerAngles, EGameObjectReferences referenceCategory)
    {
        switch (referenceCategory)
        {
            case EGameObjectReferences.ResourceBuildings: return this.resourceBuildings.Instantiate(gameobjectName, position, eulerAngles);
            case EGameObjectReferences.UI: return this.UI.Instantiate(gameobjectName, position, eulerAngles);
        }

        return null; 
    }

    public GameObject Instantiate(string gameobjectName, Vector3 position, Vector3 eulerAngles, Transform parent, EGameObjectReferences referenceCategory)
    {
        switch (referenceCategory)
        {
            case EGameObjectReferences.ResourceBuildings: return this.resourceBuildings.Instantiate(gameobjectName, position, eulerAngles, parent);
            case EGameObjectReferences.UI: return this.UI.Instantiate(gameobjectName, position, eulerAngles, parent);
        }

        return null; 
    }

    public GameObject Instantiate(string gameobjectName, Vector3 position, Vector3 rotation, Vector3 scale, Transform parent, EGameObjectReferences referenceCategory)
    {
        switch (referenceCategory)
        {
            case EGameObjectReferences.ResourceBuildings: return this.resourceBuildings.Instantiate(gameobjectName, position, rotation, scale, parent);
            case EGameObjectReferences.UI: return this.UI.Instantiate(gameobjectName, position, rotation, scale, parent);
        }

        return null; 
    }

    public GameObject Instantiate(string gameobjectName, Transform parent, EGameObjectReferences referenceCategory)
    {
        switch (referenceCategory)
        {
            case EGameObjectReferences.ResourceBuildings: return this.resourceBuildings.Instantiate(gameobjectName,  parent);
            case EGameObjectReferences.UI: return this.UI.Instantiate(gameobjectName, parent);
        }

        return null; 
    }
    #endregion
}