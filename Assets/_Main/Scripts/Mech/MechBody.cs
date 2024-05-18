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
            var myTransform = transform;
            
            var lookAtDirection = raycastHit.point - myTransform.position;
            transform.forward = Vector3.Slerp(myTransform.forward,  lookAtDirection, rotationSpeed * Time.deltaTime);
        }
    }
}
