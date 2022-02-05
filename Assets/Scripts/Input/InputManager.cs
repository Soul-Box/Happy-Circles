using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;

[Serializable]
public class StartTouchEvent : UnityEvent<Vector2>
{
}

[Serializable]
public class EndTouchEvent : UnityEvent<Vector2>
{
}

[DefaultExecutionOrder(-1)]
public class InputManager : MonoBehaviour
{
    private InputControls _controls;

    private Camera _camera;

    [SerializeField] private StartTouchEvent startTouchEvent;
    [SerializeField] private StartTouchEvent endTouchEvent;


    private void Awake()
    {
        _camera = Camera.main;
        _controls = new InputControls();
    }

    private void OnEnable()
    {
        _controls.Touch.Enable();
    }

    private void OnDisable()
    {
        _controls.Touch.Disable();
    }

    private void Start()
    {
        _controls.Touch.TouchPress.performed += StartTouch;
        _controls.Touch.TouchPress.performed += EndTouch;
    }

    private void StartTouch(InputAction.CallbackContext ctx)
    {
        if (startTouchEvent == null) return;

        startTouchEvent.Invoke(TouchPosition());
    }

    private void EndTouch(InputAction.CallbackContext obj)
    {
        if (endTouchEvent == null) return;

        endTouchEvent.Invoke(TouchPosition());
    }

    public Vector2 TouchPosition()
    {
        return Utils.ScreenToWorld(_camera, _controls.Touch.TouchPosition.ReadValue<Vector2>());
    }
}