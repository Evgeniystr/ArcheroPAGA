using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEngine.EventSystems;


public class JoystickTrigger : MonoBehaviour, IPointerDownHandler, IPointerUpHandler, IDragHandler
{
    [HideInInspector]
    public Action clickEvent;
    [HideInInspector]
    public Action releaseEvent;
    [HideInInspector]
    public Action dragEvent;


    public void OnPointerDown(PointerEventData eventData)
    {
        clickEvent();
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        releaseEvent();
    }

    public void OnDrag(PointerEventData eventData)
    {
        dragEvent();
    }
}
