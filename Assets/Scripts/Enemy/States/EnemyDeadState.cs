using UnityEngine;

public class EnemyDeadState : EnemyState
{
    private Collider2D _collider;
    public EnemyDeadState(Enemy enemy, StateMachine stateMachine) 
        : base(enemy, stateMachine, "Dead", string.Empty)
    {
        _collider = Enemy.GetComponent<Collider2D>();
    }

    public override void Enter()
    {
        base.Enter();
        
        Animator.enabled = false;
        _collider.enabled = false;
        Rb.gravityScale = 12f;
        Enemy.SetVelocityY(15f);
        
        StateMachine.SwitchOffStateMachine();
    }
}