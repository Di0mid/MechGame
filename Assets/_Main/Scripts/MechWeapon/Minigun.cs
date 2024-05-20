using System;
using UnityEngine;
using Random = UnityEngine.Random;

public class Minigun : MechWeapon
{
    [SerializeField] private float requiredMuzzleRevolutions;
    [SerializeField] private float muzzleRevolutionsIncreaseSpeed;

    private float _currentMuzzleRevolutions;
    
    public override void Shooting()
    {
        _currentMuzzleRevolutions += muzzleRevolutionsIncreaseSpeed * Time.deltaTime;
        
        if(_currentMuzzleRevolutions < requiredMuzzleRevolutions)
            return;
        
        if (!(lastShootTime <= Time.time + data.timeBetweenShoot)) 
            return;
        
        var needAmmo = muzzleOutList.Count * data.shotsPerMuzzle;
        
        if (currentAmmo < needAmmo)
            return;
        
        foreach (var muzzle in muzzleOutList)
        {
            for (var i = 0; i < data.shotsPerMuzzle; i++)
            {
                var shootDirection = muzzle.forward + Random.insideUnitSphere * currentSpread;
                var ray = new Ray(muzzle.position, shootDirection);
                
                if (Physics.Raycast(ray, out var raycastHit, data.distance, data.targetLayerMask))
                {
                    Debug.DrawLine(transform.position, raycastHit.point, Color.red);
                        
                    if (raycastHit.collider.TryGetComponent<IDamageable>(out var target))
                    {
                        target.TakeDamage(data.damage);
                    }
                }
                else
                {
                    Debug.DrawLine(transform.position, ray.GetPoint(data.distance), Color.red);
                }
                
            }
        }
        
        currentSpread += data.spreadIncreaseSpeed * Time.deltaTime;
                
        if (currentSpread > data.maxSpread)
            currentSpread = data.maxSpread;
        
        lastShootTime = Time.time;
        currentAmmo -= needAmmo;
        
    }
}
