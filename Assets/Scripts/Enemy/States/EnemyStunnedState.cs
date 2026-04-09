using UnityEngine;

public class EnemyStunnedState : EnemyState
{
    private readonly EnemyVFX _enemyVFX;
    
    public EnemyStunnedState(Enemy enemy, StateMachine stateMachine) 
        : base(enemy, stateMachine, "Stunned", "stunned")
    {
        _enemyVFX = enemy.GetComponent<EnemyVFX>();
    }

    public override void Enter()
    {
        base.Enter();
        
        _enemyVFX.EnableAttackAlert(false);
        Enemy.EnableCounterWindow(false);
        stateTimer = Enemy.stunnedDuration;
        Enemy.SetVelocity(Enemy.stunnedVelocity.x * -Enemy.FacingDir, Enemy.stunnedVelocity.y);
    }

    public override void Update(float deltaTime)
    {
        base.Update(deltaTime);
        
        if (stateTimer <= 0f)
            Enemy.ChangeToIdleState();
    }
}