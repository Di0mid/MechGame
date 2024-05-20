using System;
using UnityEngine;

public class FollowMouse : MonoBehaviour
{
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
        var ray = Camera.main.ScreenPointToRay(_input.Player.MousePosition.ReadValue<Vector2>());
        if (Physics.Raycast(ray, out var raycastHit))
        {
            transform.up = raycastHit.normal;
            transform.position = raycastHit.point;
        }
    }
}
