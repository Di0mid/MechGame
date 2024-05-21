using System;
using System.Collections.Generic;
using UnityEngine;

public abstract class MechWeaponBase : MonoBehaviour
{
    [SerializeField] protected WeaponSO data;

    [Space] 
    [SerializeField] protected List<Transform> muzzleOutList;
    
    [Space]
    [SerializeField] protected LayerMask targetLayerMask;

    protected int currentAmmo;
    protected float currentSpread;
    protected float lastShootTime;

    private void Start()
    {
        currentAmmo = data.ammo;
        currentSpread = data.baseSpread;
    }

    public abstract void Shooting();

    public int GetMuzzleCount()
    {
        return muzzleOutList.Count;
    }
}
