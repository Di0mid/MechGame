using System;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(SphereCollider))]
public class Turret : MonoBehaviour
{
    [SerializeField] private TurretSO data;
    
    [Space]
    [SerializeField] private Transform xRotatablePart;
    [SerializeField] private Transform yRotatablePart;

    private enum State { Idle, ChoseClosestTarget, Shooting }
    [SerializeField] private State state;

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
        state = State.Idle;
    }

    private void Update()
    {
        switch (state)
        {
            case State.Idle:
                if (_targetsInRange.Count > 0)
                    state = State.ChoseClosestTarget;
                break;
            case State.ChoseClosestTarget:
                ChoseClosestTarget();
                if(_currentTarget != null)
                    state = State.Shooting;
                break;
            case State.Shooting:
                Shooting();
                break;
            default:
                throw new ArgumentOutOfRangeException();
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
        if (_currentTarget == null)
            state = State.Idle;

        HandleRotation();
        
        
    }

    private void HandleRotation()
    {
        var currentTargetPosition = _currentTarget.transform.position;
        
        var direction = _currentTarget.transform.position - yRotatablePart.position;
        var rotation = Quaternion.LookRotation(direction);
        rotation.x = 0;
        rotation.z = 0;
        yRotatablePart.localRotation = rotation;

        direction = _currentTarget.transform.position - xRotatablePart.position;
        rotation = Quaternion.LookRotation(direction);
        rotation.y = 0;
        rotation.z = 0;
        xRotatablePart.localRotation = rotation;
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
                state = State.Idle;
            }
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, data.viewRadius);
    }
}
