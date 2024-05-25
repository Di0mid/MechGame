using UnityEngine;

[CreateAssetMenu(fileName = "newTurretData", menuName = "Data/Turret")]
public class TurretSO : ScriptableObject
{
    [Min(1)]
    public float viewRadius;
    [Min(1)]
    public float rotationSpeed;

    [Space]
    [Range(0, 360)]
    public float xRotationClampAngle;
    [Range(0, 360)] 
    public float yMinAngleToShoot;
    
    [Space] 
    public LayerMask targetLayerMask;
}
