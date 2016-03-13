using UnityEngine;
using System.Collections;

public class PlayerBuildingCreation : MonoBehaviour
{
    private GameObject buildingToCreateGameObject;
    private string buildingToCreateName;
    private ServiceLocator serviceLocator;

    public GameObject BuildingToCreateGameObject
    {
        get { return buildingToCreateGameObject; }
        private set { buildingToCreateGameObject = value; }
    }

    public string BuildingToCreateName
    {
        get { return buildingToCreateName; }
        set { buildingToCreateName = value; }
    }

    void Awake()
    {
        this.serviceLocator = GameObject.FindGameObjectWithTag("ServiceLocator").GetComponent<ServiceLocator>();
    }

    public void ActiveBuildingToCreate()
    {
        this.buildingToCreateGameObject.SetActive(true);
    }

    public void UnactiveBuildingToCreate()
    {
        this.buildingToCreateGameObject.SetActive(false);
    }

    public void InstantiateBuilding()
    {
        if (null != this.buildingToCreateGameObject)
            Destroy(buildingToCreateGameObject);

        this.buildingToCreateGameObject = this.serviceLocator.GameObjectManager.Instantiate(this.buildingToCreateName);

        this.buildingToCreateGameObject.AddComponent<FollowCursorPosition>();
    }

    public void AddBuilding()
    {
        if (null != this.buildingToCreateGameObject)
        {
            Destroy(this.buildingToCreateGameObject.GetComponent<FollowCursorPosition>());
            // if (PlayerBuildingContainer.PlaceBuilding(this.buildingToCreateGameObject))
            //  Destroy(this.buildingToCreateGameObject);

            // PlayerBuildingContainer.PlayerBuilding(GameObject objectToCreate)
            // bool objectExit = null != objectToCreate;
            // bool canPayObject= false;
            // if (objectExit)
            // {
            //  PlayerResource = GetComponent<PlayerResource>();
            //  BuildingDataParameter = objectToCreate.GetComponent<DataClass>();
          
            // Sous Méthode : Pay appele canPay et retourne si il a pu payé
            //  if (GetPlayerResource.CanPay(buildingDataParameter)
            //  {
            //      GetPlayerResource.Pay(buildingDataParameter);
            //      canPayObject = true;
            //  }
            //      
            // return objectExit && canPayObjct && canPlaceObject;
            // Communiquer avec if (PlayerBuildingContainer.PlaceBuilding(this.buildingToCreateGameObject);
            // Ce dernier va placer le bâtiment si il 
        }
    }
        //Follow cursor
}
