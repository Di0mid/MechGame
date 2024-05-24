using UnityEngine;

public class MechBody : MonoBehaviour
{
    [SerializeField] private Mech mech;

    [Space]
    [SerializeField] private float rotationSpeed;
    [SerializeField] private Transform rotatablePart;
    
    [Space]
    [SerializeField] private float rayMaxDistance;
    [SerializeField] private LayerMask rayLayerMask;
    
    private void Start()
    {
        mech.OnLookAtInput += MechOnLookAtInput;
    }

    private void MechOnLookAtInput(object sender, Vector2 e)
    {
        var ray = Camera.main.ScreenPointToRay(e);
        if (Physics.Raycast(ray, out var raycastHit, rayMaxDistance, rayLayerMask))
        {
            var targetDirection = raycastHit.point - rotatablePart.position;
            var targetRotation = Quaternion.LookRotation(targetDirection.normalized);
            
            rotatablePart.rotation = Quaternion.Slerp(rotatablePart.rotation,  targetRotation, rotationSpeed * Time.deltaTime);
        }
    }
}
