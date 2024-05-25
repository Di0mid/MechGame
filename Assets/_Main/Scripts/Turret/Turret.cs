using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

[RequireComponent(typeof(SphereCollider))]
public class Turret : MonoBehaviour
{
    [SerializeField] private TurretSO data;
    
    [Space]
    [SerializeField] private WeaponBase weapon;

    [Space]
    [SerializeField] private Transform xRotatablePart;
    [SerializeField] private Transform yRotatablePart;

    private enum State { Idle, ChoseClosestTarget, Shooting }
    private State _state;

    private readonly List<TestTarget> _targetsInRange = new();
    private TestTarget _currentTarget;

    private SphereCollider _sphereCollider;
    
    private void Awake()
    {
        _sphereCollider = GetComponent<SphereCollider>();
        _sphereCollider.isTrigger = true;
        _sphereCollider.radius = data.viewRadius;
    }

    private void Start()
    {
        _state = State.Idle;
    }

    private void Update()
    {
        HandleStates();
    }

    private void HandleStates()
    {
        switch (_state)
        {
            case State.Idle:
                
                if (_targetsInRange.Count > 0)
                    _state = State.ChoseClosestTarget;
                break;
            
            case State.ChoseClosestTarget:
                
                ChoseClosestTarget();
                if(_currentTarget != null)
                    _state = State.Shooting;
                break;
            
            case State.Shooting:
                
                if (_currentTarget == null)
                    _state = State.Idle;
                Shooting();
                break;
        }
    }
    
    private void ChoseClosestTarget()
    {
        if(_currentTarget != null)
            return;

        var minDistance = Mathf.Infinity;
        foreach (var target in _targetsInRange)
        {
            var distanceToTarget = Vector3.Distance(transform.position, target.transform.position);
            if (distanceToTarget < minDistance)
            {
                _currentTarget = target;
                minDistance = distanceToTarget;
            }
        }
    }

    private void Shooting()
    {
        HandleRotation();

        var directionToTarget = _currentTarget.transform.position - yRotatablePart.position;
        if (Vector3.Angle(yRotatablePart.forward, directionToTarget.normalized) <= data.yMinAngleToShoot)
        {
            weapon?.Shooting();
        }
    }

    private void HandleRotation()
    {
        var currentTargetPosition = _currentTarget.transform.position;
        
        var directionToTarget = currentTargetPosition - yRotatablePart.position;
        var rotation = Quaternion.LookRotation(directionToTarget.normalized);
        
        rotation.x = 0;
        rotation.z = 0;
        yRotatablePart.localRotation =
            Quaternion.Slerp(yRotatablePart.localRotation, rotation, data.rotationSpeed * Time.deltaTime);

        directionToTarget = currentTargetPosition - xRotatablePart.position;
        rotation = Quaternion.LookRotation(directionToTarget.normalized);
        
        rotation.y = 0;
        rotation.z = 0;
        xRotatablePart.localRotation =
            Quaternion.Slerp(xRotatablePart.localRotation, rotation, data.rotationSpeed * Time.deltaTime);
    }
    
    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent<TestTarget>(out var target))
        {
            if (!_targetsInRange.Contains(target))
            {
                _targetsInRange.Add(target);
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.TryGetComponent<TestTarget>(out var target))
        {
            if (_targetsInRange.Contains(target))
            {
                _targetsInRange.Remove(target);
            }

            if (_currentTarget.Equals(target))
            {
                _currentTarget = null;
                _state = State.Idle;
            }
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, data.viewRadius);
    }
}
