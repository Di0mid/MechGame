using System;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    private float _speed;
    private int _damage;
    private Vector3 _movementDirection;

    private bool _isLaunched;

    private void FixedUpdate()
    {
        if(_isLaunched)
            HandleMovement();
    }

    public void Launch(float speed, int damage, Vector3 direction)
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
        var traveledDistance = _speed * Time.fixedDeltaTime;
        transform.position += traveledDistance * _movementDirection;

        if (Physics.SphereCast(transform.position, 0.05f, _movementDirection, out var raycastHit, traveledDistance))
        {
            if (raycastHit.collider.TryGetComponent<IDamageable>(out var damageable))
            {
                damageable.TakeDamage(_damage);
            }
            
            Destroy(gameObject);
        }
    }
}
