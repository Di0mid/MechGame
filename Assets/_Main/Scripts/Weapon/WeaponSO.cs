using System;
using UnityEngine;
using UnityEngine.Serialization;

[CreateAssetMenu(fileName = "newWeaponData", menuName = "Data/Weapon")]
public class WeaponSO : ScriptableObject
{
    [Min(1)]
    public int damage;
    
    [Space]
    [Min(1)]
    public int ammo;

    [Space] 
    [Min(1)]
    public int shotsPerMuzzle;

    [Space] 
    [Range(0, 0.5f)]
    public float baseSpread;
    [Range(0, 1)]
    public float maxSpread;
    [Range(0, 0.1f)]
    public float spreadIncreaseSpeed;

    [Space] 
    [Min(0.01f)]
    public float timeBetweenShoot;

    [Space] 
    [Min(1)]
    public int rayDistance;

    [Space]
    public FireMode fireMode;
    public enum FireMode
    {
        Projectile,
        Raycast
    }

    [Space] 
    [Min(1)]
    public float projectileSpeed;
    public Projectile projectile;

    private void OnValidate()
    {
        if (baseSpread > maxSpread)
            maxSpread = baseSpread;
    }
}
