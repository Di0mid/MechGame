using UnityEngine;

[CreateAssetMenu(fileName = "newMechWeaponDara", menuName = "Mech/Data/Weapon")]
public class MechWeaponSO : ScriptableObject
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
    public int distance;
    
    [Space] 
    public LayerMask targetLayerMask;
}
