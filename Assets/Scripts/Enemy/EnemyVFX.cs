using UnityEngine;

public class EnemyVFX : EntityVFX
{
    [Header("Counter Attack Window")] 
    [SerializeField] private GameObject attackAlert;

    protected override void Awake()
    {
        base.Awake();
        
        EnableAttackAlert(false);
    }

    public void EnableAttackAlert(bool enable)
    {
        attackAlert?.SetActive(enable);
    }
}