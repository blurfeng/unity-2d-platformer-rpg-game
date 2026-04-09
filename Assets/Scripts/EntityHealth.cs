using UnityEngine;

public class EntityHealth : MonoBehaviour
{
    [SerializeField] protected float maxHealth = 100f;
    [SerializeField] protected bool isDead;
    
    [Header("On damage knockback")]
    [SerializeField] private Vector2 knockbackForce = new Vector2(1.5f, 2.5f);
    [SerializeField] private Vector2 heavyKnockbackForce = new Vector2(7f, 7f);
    [SerializeField] private float knockbackDuration = 0.2f;
    [SerializeField] private float heavyKnockbackDuration = 0.5f;
    
    [Header("On heavy damage")]
    [SerializeField] private float heavyDamageThreshold = 0.3f;
    
    private EntityVFX _entityVFX;
    private Entity _entity;
    private float _currentHealth;

    protected virtual void Awake()
    {
        _entityVFX = gameObject.GetComponent<EntityVFX>();
        _entity = GetComponent<Entity>();
        _currentHealth = maxHealth;
    }

    public virtual void TakeDamage(float damage, Transform damageDealer)
    {
        if (isDead)
            return;
        
        ReduceHealth(damage);
        
        Vector2 knockback = CalculateKnockback(damage, damageDealer);
        _entity?.ReceiveKnockback(knockback, CalculateDuration(damage));
        
        _entityVFX?.PlayOnDamageVfs();
    }

    protected void ReduceHealth(float damage)
    {
        _currentHealth -= damage;
        
        if (_currentHealth <= 0)
            Die();
    }

    private void Die()
    {
        isDead = true;
        Debug.Log(gameObject.name + " has died.");
    }

    private Vector2 CalculateKnockback(float damage, Transform damageDealer)
    {
        int direction = transform.position.x > damageDealer.position.x ? 1 : -1;

        Vector2 knockback = IsHeavyDamage(damage) ? heavyKnockbackForce : knockbackForce;
        knockback.x *= direction;
        return knockback;
    }

    private float CalculateDuration(float damage)
    {
        return IsHeavyDamage(damage) ? heavyKnockbackDuration : knockbackDuration;
    }
    
    private bool IsHeavyDamage(float damage) => damage / maxHealth > heavyDamageThreshold;
}