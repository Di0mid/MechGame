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
        
        var targetRotation = Quaternion.LookRotation(movementDirection);
        transform.rotation = Quaternion.Slerp( transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);
        
        var canMove = Quaternion.Angle( transform.rotation, targetRotation) <= minAngleToMove;
        
        if (canMove)
        {
            mech.transform.position += movementDirection * (movementSpeed * Time.deltaTime);
        }
    }
}
