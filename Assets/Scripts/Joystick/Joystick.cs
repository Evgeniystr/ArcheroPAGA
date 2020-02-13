using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using UnityEngine.Events;

public class Joystick : MonoBehaviour
{
    [SerializeField]
    Image innerCircle, outerCircle;

    const float trashhold = 0.05f;

    JoystickTrigger joystickTrigger;
    Vector3 startPoint;
    Vector3 defaultPosition;
        
    enum UIelement { InnerCircle, OuterCircle}

    public float horizontalDirection { get; private set; }
    public float verticalDirection { get; private set; }
    public bool isInputActive { get; private set; }


    private void Start()
    {
        defaultPosition = innerCircle.transform.position;
        joystickTrigger = GetComponentInChildren<JoystickTrigger>();

        joystickTrigger.clickEvent += OnTouch;
        joystickTrigger.releaseEvent += OnRelease;
        joystickTrigger.dragEvent += OnDrag;
    }


    void OnTouch()
    {
        startPoint = Camera.main.WorldToScreenPoint(Input.mousePosition);

        innerCircle.transform.position = Input.mousePosition;
        outerCircle.transform.position = Input.mousePosition;
    }
    private void OnDrag()
    {
        //move inner circle
        innerCircle.transform.position = Input.mousePosition;

        //update direction
        var direction = (Camera.main.WorldToScreenPoint(Input.mousePosition) - startPoint);

        isInputActive = Mathf.Abs(direction.x) >= trashhold && Mathf.Abs(direction.y) >= trashhold;

        if(isInputActive)
        {
            horizontalDirection = direction.normalized.x;
            verticalDirection = direction.normalized.y;
        }
    }

    void OnRelease()
    {
        isInputActive = false;
        innerCircle.transform.position = defaultPosition;
        outerCircle.transform.position = defaultPosition;
    }
}
