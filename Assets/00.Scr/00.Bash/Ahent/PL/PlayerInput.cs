using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerInput :MonoSingleton<PlayerInput>, Input2.IActionsActions,Input2.IUtilsActions
{
    Input2 _input;
    public Vector2 mouseMov,mousePos;
    public Vector2 movement;

    public bool isFire,isSliding;

    public Action Jump;

    public bool canRotate;

    protected override void Awake()
    {
        _input = new Input2();
        _input.Actions.SetCallbacks(this);
        _input.Enable();
        base.Awake();
    }

    void Start()
    {
        
    }

    public void OnEsc(InputAction.CallbackContext context)
    {
        
    }

    public void OnMouseButton(InputAction.CallbackContext context)
    {
        if (context.started)
        {
            isFire = true;
        }
        if (context.canceled) 
        {
            isFire = false;
        }
    }

    public void OnMovement(InputAction.CallbackContext context)
    {
        movement = context.ReadValue<Vector2>();
    }

    public void OnSliding(InputAction.CallbackContext context)
    {
        if (context.started)
        {
            isSliding = true;
        }
        if (context.canceled)
        {
            isSliding = false;
        }
    }

    public void OnSwap(InputAction.CallbackContext context)
    {
        
    }

    public void OnJump(InputAction.CallbackContext context)
    {
        if (context.performed)
            Jump?.Invoke();
    }

    public void OnDash(InputAction.CallbackContext context)
    {
        
    }

    public void OnHotBar(InputAction.CallbackContext context)
    {
        print(context.ReadValue<int>());
    }

    public void OnMouseMove(InputAction.CallbackContext context)
    {
        if(canRotate == false)return;
        
        mouseMov = context.ReadValue<Vector2>();
    }
}
