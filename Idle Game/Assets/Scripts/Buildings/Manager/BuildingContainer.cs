using System;
using System.Collections.Generic;
using UnityEngine;

public class BuildingContainer
{
    #region Fields
    private List<ABuilding> Buildings;
    #endregion

    #region Constructor
    public BuildingContainer()
    {
        this.Buildings = new List<ABuilding>();
    }
    #endregion

    #region Behaviour Methods
    public void AddBuilding(ABuilding building)
    {
        this.Buildings.Add(building);
    }

    public void RemoveBuilding(ABuilding building)
    {
        this.Buildings.Remove(building);
    }
    #endregion
}