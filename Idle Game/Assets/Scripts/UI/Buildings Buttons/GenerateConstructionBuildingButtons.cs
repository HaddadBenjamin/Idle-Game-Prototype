using UnityEngine;

public class GenerateConstructionBuildingButtons : MonoBehaviour
{
    #region Fields
    [SerializeField]
    private string[] prefabsName;
    #endregion

    #region Constructor
    #endregion

    #region Properties
    #endregion

    #region Unity Methods
    void Start()
    {
        Transform myTransform = transform;

        for (int prefabNameIndex = 0; prefabNameIndex < this.prefabsName.Length; prefabNameIndex++)
        {
            ServiceContainer.Instance.GameObjectReferencesArrays.Instantiate("Building Button",
                new Vector3(50.0f + 216.0f * prefabNameIndex, 52.0f, 0.0f),
                Vector3.zero,
                new Vector3(0.81f, 0.81f, 0.81f), //C'est immonde
                myTransform,
                EGameObjectReferences.UI).GetComponent<BuildingButton>().Initialize(this.prefabsName[prefabNameIndex]);
        }
    }
    #endregion

    #region Behaviour Methods
    #endregion
}