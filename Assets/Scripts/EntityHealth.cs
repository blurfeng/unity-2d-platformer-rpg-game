using UnityEngine;

public class EntityHealth : MonoBehaviour
{
    [SerializeField] protected float maxHealth = 100f;
    [SerializeField] protected bool isDead;

    public virtual void TakeDamage(float damage, Transform damageDealer)
    {
        if (isDead)
            return;
        
        ReduceHealth(damage);
    }

    protected void ReduceHealth(float damage)
    {
        maxHealth -= damage;
        
        if (maxHealth <= 0)
            Die();
    }

    private void Die()
    {
        isDead = true;
        Debug.Log(gameObject.name + " has died.");
    }
}