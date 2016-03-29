using System;
using UnityEngine;

public class GenerateCraftEquipmentCaseButtons : MonoBehaviour
{
    #region Fields
    [SerializeField]
    private int numberOfCase;
    [SerializeField]
    private float offsetX;
    private CraftEquipmentCaseButton[] craftEquipmentCases;
    #endregion

    #region Unity Methods
    void Awake()
    {
        this.craftEquipmentCases = new CraftEquipmentCaseButton[this.numberOfCase];

        this.Generate();
    }
    #endregion

    #region Behaviour Methods

    private void Generate()
    {
        Transform myTransform = transform;
        int middleCase = this.numberOfCase / 2;

        for (int caseIndex = 0; caseIndex < this.numberOfCase; caseIndex++)
        {
            GameObject equipmentCaseButtonGameObject = ServiceContainer.Instance.GameObjectReferencesArrays.Instantiate(
                "Craft Equipment Case Button",
                // Permet de centrer le générateur sur l'axe horizontal
                new Vector3(
                    caseIndex + 1 >= middleCase ? caseIndex * this.offsetX :
                    caseIndex + 1 < middleCase ? caseIndex * this.offsetX :
                        0.0f,
                    0.0f, 0.0f),
                Vector3.zero,
                Vector3.one,
                myTransform,
                EGameObjectReferences.UI);

            this.craftEquipmentCases[caseIndex] = equipmentCaseButtonGameObject.GetComponent<CraftEquipmentCaseButton>();
        }
    }

    public CraftEquipmentCaseButton GetAvailableCase()
    {
        return Array.Find(this.craftEquipmentCases, equipmentCase => ECraftEquipmentMode.Waiting == equipmentCase.CraftEquipmentMode);
    }
    #endregion
}