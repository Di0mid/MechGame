using UnityEngine;

public class Shotgun : MechWeaponBase
{
    public override void Shooting()
    {
        if (!(Time.time > lastShootTime)) 
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
        }
        
        currentSpread += data.spreadIncreaseSpeed * Time.deltaTime;
                
        if (currentSpread > data.maxSpread)
            currentSpread = data.maxSpread;
        
        lastShootTime = Time.time + data.timeBetweenShoot;
        currentAmmo -= needAmmo;
    }
}
