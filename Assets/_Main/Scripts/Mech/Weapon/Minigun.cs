using UnityEngine;

public class Minigun : WeaponBase
{
    [SerializeField] private float requiredMuzzleRevolutions;
    [SerializeField] private float muzzleRevolutionsIncreaseSpeed;

    private float _currentMuzzleRevolutions;
    
    public override void Shooting()
    {
        _currentMuzzleRevolutions += muzzleRevolutionsIncreaseSpeed * Time.deltaTime;
        
        if(_currentMuzzleRevolutions < requiredMuzzleRevolutions)
            return;
        
        base.Shooting();
    }
}
