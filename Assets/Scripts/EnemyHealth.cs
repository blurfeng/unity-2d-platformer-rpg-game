using UnityEngine;

public class EnemyHealth : EntityHealth
{
    private Enemy Enemy => GetComponent<Enemy>();
    
    public override void TakeDamage(float damage, Transform damageDealer)
    {
        if (damageDealer.GetComponent<Player>())
        {
            Enemy.TryEnterBattleState(damageDealer);
        }
        
        base.TakeDamage(damage, damageDealer);
    }
}