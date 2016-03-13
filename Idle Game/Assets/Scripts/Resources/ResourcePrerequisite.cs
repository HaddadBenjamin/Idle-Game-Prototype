using UnityEngine;
using System.Collections;

[System.Serializable]
public class ResourcePrerequisite
{
    [SerializeField]
    private int requieredNumber;
    [SerializeField]
    private EResourceCategory resourceCategory;
}
