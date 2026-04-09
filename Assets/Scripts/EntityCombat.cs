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
            EntityHealth entityHealth = co.GetComponent<EntityHealth>();
            if (entityHealth)
            {
                entityHealth.TakeDamage(damage);
            }
        }
    }

    private Collider2D[] GetDetectedColliders()
    {
        return Physics2D.OverlapCircleAll(targetCheck.position, targetCHeckRadius, targetLayer);
    }

    private void OnDrawGizmos()
    {
        if (targetCheck) Gizmos.DrawWireSphere(targetCheck.position, targetCHeckRadius);
    }
}