using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class Mech : MonoBehaviour
{
    public event EventHandler<Vector2> OnMovementInput;
    public event EventHandler<Vector2> OnLookAtInput;
    public event EventHandler<InputAction> OnLeftHandInput;
    public event EventHandler<InputAction> OnRightHandInput;
    
    private GameInput _input;

    private void Awake()
    {
        _input = new GameInput();
    }

    private void Start()
    {
        _input.Player.Enable();
    }

    private void Update()
    {
        HandleMovementInput();
        HandleLookAtInput();
        HandleLeftHandInput();
        HandleRightHandInput();
    }

    private void HandleMovementInput()
    {
        OnMovementInput?.Invoke(this, _input.Player.Movement.ReadValue<Vector2>());
    }

    private void HandleLookAtInput()
    {
        OnLookAtInput?.Invoke(this, _input.Player.MousePosition.ReadValue<Vector2>());
    }

    private void HandleLeftHandInput()
    {
        OnLeftHandInput?.Invoke(this, _input.Player.LeftHand);
    }
    
    private void HandleRightHandInput()
    {
        OnRightHandInput?.Invoke(this, _input.Player.RightHand);
    }
}

