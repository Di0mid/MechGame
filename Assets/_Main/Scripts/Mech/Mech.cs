using System;
using UnityEngine;

public class Mech : MonoBehaviour
{
    public event EventHandler<Vector2> OnMovementInput;
    public event EventHandler<Vector2> OnLookAtInput;
    
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
    }

    private void HandleMovementInput()
    {
        OnMovementInput?.Invoke(this, _input.Player.Movement.ReadValue<Vector2>());
    }

    private void HandleLookAtInput()
    {
        OnLookAtInput?.Invoke(this, _input.Player.LookAt.ReadValue<Vector2>());
    }
}
