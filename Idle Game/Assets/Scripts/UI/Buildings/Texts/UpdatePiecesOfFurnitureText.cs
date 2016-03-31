﻿using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class UpdatePiecesOfFurnitureText : MonoBehaviour
{
    [SerializeField]
    private BuildingsAnalytic playerBuildingsAnalytic;
    private Text text;

    void Start()
    {
        this.playerBuildingsAnalytic = ServiceContainer.Instance.GameObjectReferencesArrayInScene.Get("[PLAYER]", EGameObjectReferences.Rest).GetComponent<PlayerBuildingsManager>().BuildingsAnalytic;

        this.text = GetComponent<Text>();

        this.playerBuildingsAnalytic.PiecesOfFurniture.CurrentValueModificationDelegate += this.UpdateQuantityText;
    }

    private void UpdateQuantityText(int current, int maximum)
    {
        this.text.text = "Capacity " + current + " / " + maximum;
    }
}

