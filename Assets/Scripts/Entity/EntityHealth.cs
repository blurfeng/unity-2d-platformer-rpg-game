using UnityEngine;
using UnityEngine.UI;

public class EntityHealth : MonoBehaviour, IDamageable
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

    private Slider _healthBar;

    protected virtual void Awake()
    {
        _entityVFX = gameObject.GetComponent<EntityVFX>();
        _entity = GetComponent<Entity>();
        _healthBar = GetComponentInChildren<Slider>();
        
        _currentHealth = maxHealth;
        UpdateHealthBar();
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
        UpdateHealthBar();
        
        if (_currentHealth <= 0)
            Die();
    }

    private void UpdateHealthBar()
    {
        if (!_healthBar) return;
        
        _healthBar.value = _currentHealth / maxHealth;
    }

    private void Die()
    {
        isDead = true;
        _entity.Death();
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