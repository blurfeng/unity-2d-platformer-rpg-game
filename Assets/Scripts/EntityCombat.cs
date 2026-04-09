using System;
using UnityEngine;

public class EntityCombat : MonoBehaviour
{
    [Header("Target detection")]
    [SerializeField] private Transform targetCheck;
    [SerializeField] private float targetCHeckRadius = 1f;
    [SerializeField] private LayerMask targetLayer;
    
    public void PerformAttack()
    {
        foreach (Collider2D co in GetDetectedColliders())
        {
            Debug.Log("Attacked " + co.name);
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