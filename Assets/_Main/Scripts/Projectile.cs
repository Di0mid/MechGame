using System;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    [SerializeField] private float checkRadius;

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
        var previousPosition = transform.position;
        transform.position += _movementDirection * (_speed * Time.fixedDeltaTime);
        var passedDistance = Vector3.Distance(previousPosition, transform.position);
        
        if (Physics.SphereCast(previousPosition, checkRadius, _movementDirection, out var raycastHit, passedDistance))
        {
            if (raycastHit.collider.TryGetComponent<IDamageable>(out var damageable))
            {
                damageable.TakeDamage(_damage);
            }
            
            Destroy(gameObject);
        }
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, checkRadius);
    }
}
