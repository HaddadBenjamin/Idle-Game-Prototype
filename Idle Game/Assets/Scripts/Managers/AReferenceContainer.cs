using UnityEngine;
using System.Collections;
using System;

public abstract class AReferenceContainer<ReferenceClass> : AServiceComponent where ReferenceClass : UnityEngine.Object
{
    [SerializeField]
    protected ReferenceClass[] references;
    protected int[] hashIds;

    public override void InitializeByServiceLocator()
    {
        ObjectContainerHelper.InitializeHashIds(
            Array.ConvertAll(this.references, reference => reference.name),
            ref this.hashIds);
    }

    public ReferenceClass Get(string refenceName)
    {
        return this.references[ObjectContainerHelper.GetHashCodeIndex(refenceName, ref hashIds)];
    }
}