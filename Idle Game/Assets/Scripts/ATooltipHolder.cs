﻿using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ATooltilpHolder : MonoBehaviour
{
    #region Fields
    protected GameObject tooltilpGameObject;
    protected Transform tooltilpTransform;

    protected bool onPointerEnter;
    #endregion

    #region Unity Methods
    public void Initialize(UICallbackData[] UICallbacksData)
    {
        this.tooltilpGameObject = transform.Find("Tooltip").gameObject;
        this.tooltilpTransform = this.tooltilpGameObject.transform;

        UICallbackHelper.AddCallbacksToEventTrigger(GetComponent<EventTrigger>(), UICallbacksData);
    }
    #endregion

    #region Unity Methods
    protected void UpdateTooltipPosition()
    {
        if (this.onPointerEnter)
            this.tooltilpTransform.position = Input.mousePosition;
    }
    #endregion

    #region Behaviour Methods
    protected void EnterPointerButtonAction(BaseEventData data)
    {
        this.tooltilpGameObject.SetActive(true);

        this.SetTooltilContent();

        this.onPointerEnter = true;
    }

    protected void ExitPointerButtonAction(BaseEventData data)
    {
        this.tooltilpGameObject.SetActive(false);

        this.onPointerEnter = false;
    }

    protected virtual void SetTooltilContent() { }
    #endregion
}