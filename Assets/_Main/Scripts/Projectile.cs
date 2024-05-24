using System;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    private float _speed;
    private float _damage;
    private Vector3 _movementDirection;

    private bool _isLaunched;
    
    private void Update()
    {
        if(_isLaunched)
            HandleMovement();
    }

    public void Launch(float speed, float damage, Vector3 direction)
    {
        _speed = speed;
        _damage = damage;
        _movementDirection = direction.normalized;
        
        transform.forward = _movementDirection;

        _isLaunched = true;
        
        Destroy(gameObject, 5);
    }

    private void HandleMovement()
    {
        transform.position += _movementDirection * (_speed * Time.deltaTime);
    }
}
