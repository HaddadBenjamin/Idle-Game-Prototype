using UnityEngine;
using System.Collections;
using System;

public class MaterialManager : AServiceComponent
{
    [SerializeField]
    private Material[] materials;
    private int[] hashIds;

    public override void InitializeByServiceLocator()
    {
        ObjectContainerHelper.InitializeHashIds(
            Array.ConvertAll(this.materials, material => material.name), 
            ref this.hashIds);
    }

    public Material Get(string materialName)
    {
        return this.materials[ObjectContainerHelper.GetHashCodeIndex(materialName, ref hashIds)];
    }
}


