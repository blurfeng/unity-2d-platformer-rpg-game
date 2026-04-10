using UnityEngine;

public class EntityCombat : MonoBehaviour
{
    public float damage = 10f;
    
    [Header("Target detection")]
    [SerializeField] private Transform targetCheck;
    [SerializeField] private float targetCHeckRadius = 1f;
    [SerializeField] private LayerMask targetLayer;
    
    public void PerformAttack()
    {
        foreach (Collider2D co in GetDetectedColliders())
        {
            IDamageable damageable = co.GetComponent<IDamageable>();
            if (damageable != null)
            {
                damageable.TakeDamage(damage, transform);
            }
        }
    }

    protected Collider2D[] GetDetectedColliders()
    {
        return Physics2D.OverlapCircleAll(targetCheck.position, targetCHeckRadius, targetLayer);
    }

    private void OnDrawGizmos()
    {
        if (targetCheck) Gizmos.DrawWireSphere(targetCheck.position, targetCHeckRadius);
    }
}