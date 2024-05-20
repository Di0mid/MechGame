using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class MechHands : MonoBehaviour
{
    [SerializeField] private Mech mech;
    
    [Space]
    [SerializeField] private List<MechWeaponBase> weapons;

    private void Start()
    {
        mech.OnRightHandInput += MechOnRightHandInput;
    }

    private void MechOnRightHandInput(object sender, InputAction e)
    {
        if (e.IsPressed())
        {
            if (weapons == null || weapons.Count == 0)
                return;
            
            weapons.ForEach(w => w.Shooting());
        }
    }
}
