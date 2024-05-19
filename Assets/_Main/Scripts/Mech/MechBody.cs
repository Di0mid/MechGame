using UnityEngine;

public class MechBody : MonoBehaviour
{
    [SerializeField] private Mech mech;
    
    [Space]
    [SerializeField] private float rotationSpeed;
    
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
            var targetDirection = raycastHit.point - mech.transform.position;
            var targetRotation = Quaternion.LookRotation(targetDirection);
            
            transform.rotation = Quaternion.Slerp(transform.rotation,  targetRotation, rotationSpeed * Time.deltaTime);
        }
    }
}
