using System;
using UnityEngine;

[System.Serializable]
public class ReferenceContainerThatDontUnheritFromMonoBehaviour<ReferenceClass> where ReferenceClass : UnityEngine.Object
{
    [SerializeField]
    protected ReferenceClass[] references;
    protected int[] hashIds;

    public void Initialize()
    {
        ObjectContainerHelper.InitializeHashIds(
            Array.ConvertAll(this.references, reference => reference.name),
            ref this.hashIds);
    }

    public ReferenceClass Get(string refenceName)
    {
        return this.references[ObjectContainerHelper.GetHashCodeIndex(refenceName, ref this.hashIds)];
    }
}