using UnityEngine;
using System.Collections;
using System;

public class TextureManager : AServiceComponent
{
    [SerializeField]
    private Texture2D[] textures;
    private int[] hashIds;

    public override void InitializeByServiceLocator()
    {
        ObjectContainerHelper.InitializeHashIds(
            Array.ConvertAll(this.textures, texture => texture.name), 
            ref this.hashIds);
    }

    public Texture2D Get(string textureName)
    {
        Debug.Log(this.textures[ObjectContainerHelper.GetHashCodeIndex(textureName, ref hashIds)].name);
        return this.textures[ObjectContainerHelper.GetHashCodeIndex(textureName, ref hashIds)];
    }
}


