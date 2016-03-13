using UnityEngine;
using System.Collections;
using System;

public class SpriteManager : AServiceComponent
{
    [SerializeField]
    private Sprite[] sprites;
    private int[] hashIds;

    public override void InitializeByServiceLocator()
    {
        ObjectContainerHelper.InitializeHashIds(
            Array.ConvertAll(this.sprites, texture => texture.name), 
            ref this.hashIds);
    }

    public Sprite Get(string spriteName)
    {
        return this.sprites[ObjectContainerHelper.GetHashCodeIndex(spriteName, ref hashIds)];
    }
}


