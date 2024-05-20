using UnityEngine;

public class TestTarget : MonoBehaviour, IDamageable
{
    [SerializeField] private int health;
    
    public void TakeDamage(int damage)
    {
        health -= damage;
        
        Debug.Log(name + " taked damage - " + damage);
        
        if(health <= 0)
            Destroy(gameObject);
    }
}
