using UnityEngine;
using System.Collections;

public class BuildingParameters : MonoBehaviour
{
    [SerializeField]
    private ResourcePrerequisite[] resourcesPrerequisite;
    [SerializeField]
    private string prefabName;

    public ResourcePrerequisite[] ResourcesPrerequisite
    {
        get { return resourcesPrerequisite; }
        private set { resourcesPrerequisite = value; }
    }

    public string PrefabName
    {
        get { return prefabName; }
        private set { prefabName = value; }
    }
}
