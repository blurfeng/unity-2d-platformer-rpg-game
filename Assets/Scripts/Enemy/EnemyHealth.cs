using UnityEngine;

public class EnemyHealth : EntityHealth
{
    private Enemy Enemy => GetComponent<Enemy>();
    
    public override void TakeDamage(float damage, Transform damageDealer)
    {
        base.TakeDamage(damage, damageDealer);

        if (isDead)
            return;
        
        if (damageDealer.GetComponent<Player>())
        {
            Enemy.TryEnterBattleState(damageDealer);
        }
    }
}