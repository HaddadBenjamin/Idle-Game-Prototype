using System;
using UnityEngine;

[System.Serializable]
public class SpriteReferences
{
    [SerializeField]
    protected Sprite[] references;
    protected int[] hashIds;

    public void Initialize()
    {
        ObjectContainerHelper.InitializeHashIds(
            Array.ConvertAll(this.references, reference => reference.name),
            ref this.hashIds);
    }

    public Sprite Get(string refenceName)
    {
        return this.references[ObjectContainerHelper.GetHashCodeIndex(refenceName, ref this.hashIds)];
    }
}