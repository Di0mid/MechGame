using System;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public abstract class WeaponBase : MonoBehaviour
{
    [SerializeField] protected WeaponSO data;

    [Space] 
    [SerializeField] protected List<Transform> muzzleOutList;

    [Space] 
    [SerializeField] protected LayerMask targetLayerMask;

    private int _currentAmmo;
    private float _currentSpread;
    private float _lastShootTime;

    private void Start()
    {
        _currentAmmo = data.ammo;
        _currentSpread = data.baseSpread;
    }

    public virtual void Shooting()
    {
        if (!(Time.time > _lastShootTime)) 
            return;
        
        var needAmmo = muzzleOutList.Count * data.shotsPerMuzzle;
        if (_currentAmmo < needAmmo)
            return;
        
        foreach (var muzzle in muzzleOutList)
        {
            for (var i = 0; i < data.shotsPerMuzzle; i++)
            {
                var shootDirection = muzzle.forward + Random.insideUnitSphere * _currentSpread;
                
                switch (data.fireMode)
                {
                    case WeaponSO.FireMode.Projectile:
                        
                        HandleProjectileShoot(shootDirection, muzzle);
                        break;
                    
                    case WeaponSO.FireMode.Raycast:
                        
                        HandleRaycastShoot(shootDirection, muzzle);
                        break;
                }
            }
        }
        
        _currentSpread += data.spreadIncreaseSpeed * Time.deltaTime;
        if (_currentSpread > data.maxSpread)
            _currentSpread = data.maxSpread;
        
        _lastShootTime = Time.time + data.timeBetweenShoot;
        _currentAmmo -= needAmmo;
    }

    private void HandleRaycastShoot(Vector3 shootDirection, Transform muzzle)
    {
        var ray = new Ray(muzzle.position, shootDirection);
                
        if (Physics.Raycast(ray, out var raycastHit, data.distance, targetLayerMask))
        {
            Debug.DrawLine(muzzle.position, raycastHit.point, Color.red);
                        
            if (raycastHit.collider.TryGetComponent<IDamageable>(out var target))
            {
                target.TakeDamage(data.damage);
            }
        }
        else
        {
            Debug.DrawLine(muzzle.position, ray.GetPoint(data.distance), Color.red);
        }
    }

    private void HandleProjectileShoot(Vector3 shootDirection, Transform muzzle)
    {
        var projectile = Instantiate(data.projectile, muzzle.position, Quaternion.identity);
        projectile.Launch(data.projectileSpeed, data.damage, shootDirection);
    }
    
    public int GetMuzzleCount()
    {
        return muzzleOutList.Count;
    }
}
