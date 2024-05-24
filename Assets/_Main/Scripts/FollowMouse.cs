using System;
using UnityEngine;

public class FollowMouse : MonoBehaviour
{
    [SerializeField] private float distance;
    [SerializeField] private LayerMask layerMask;
    
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
        if (Physics.Raycast(ray, out var raycastHit, distance, layerMask, QueryTriggerInteraction.Ignore))
        {
            transform.up = raycastHit.normal;
            transform.position = raycastHit.point;
        }
    }
}
