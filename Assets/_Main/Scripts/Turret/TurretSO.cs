using UnityEngine;

[CreateAssetMenu(fileName = "newTurretData", menuName = "Data/Turret")]
public class TurretSO : ScriptableObject
{
    [Min(1)]
    public float viewRadius;
    [Min(1)]
    public float rotationSpeed;

    [Space] 
    public LayerMask targetLayerMask;
}
