using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class MechHands : MonoBehaviour
{
    [SerializeField] private Mech mech;
    
    [Space]
    [SerializeField] private List<MechWeapon> weapons;

    private void Start()
    {
        mech.OnFireInput += MechOnFireInput;
    }

    private void MechOnFireInput(object sender, InputAction e)
    {
        if (e.IsPressed())
        {
            if(weapons.Count == 0)
                return;
            
            weapons.ForEach(w => w.Shooting());
        }
    }
}
