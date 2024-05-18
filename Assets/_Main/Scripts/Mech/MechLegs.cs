using UnityEngine;

public class MechLegs : MonoBehaviour
{
    [SerializeField] private Mech mech;
    
    [Space]
    [SerializeField] private float movementSpeed;
    [SerializeField] private float rotationSpeed;
    [SerializeField] private float minAngleToMove;


    private void Start()
    {
        mech.OnMovementInput += MechOnMovementInput;
    }

    private void MechOnMovementInput(object sender, Vector2 e)
    {
        var movementDirection = new Vector3(e.x, 0, e.y).normalized;
        
        if(movementDirection == Vector3.zero)
            return;

        transform.forward = Vector3.Slerp(transform.forward, movementDirection, rotationSpeed * Time.deltaTime);
        
        var canMove = Vector3.Angle(transform.forward, movementDirection) <= minAngleToMove;
        
        if (canMove)
        {
            mech.transform.position += movementDirection * (movementSpeed * Time.deltaTime);
        }
    }
}
