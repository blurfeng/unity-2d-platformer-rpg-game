using UnityEngine;

public abstract class EnemyState : EntityState
{
    private static readonly int _moveAnimSpeedMultiplier = Animator.StringToHash("moveAnimSpeedMultiplier");
    private static readonly int _velocityX = Animator.StringToHash("velocityX");
    private static readonly int _battleAnimSpeedMultiplier = Animator.StringToHash("battleAnimSpeedMultiplier");
    protected Enemy Enemy { get; private set; }
    
    public EnemyState(Enemy enemy, StateMachine stateMachine, string stateName, string animBoolName) 
        : base(stateMachine, stateName, animBoolName)
    {
        Enemy = enemy;
        Animator = Enemy.Animator;
        Rb = Enemy.Rb;
    }

    public override void UpdateAnimationParams()
    {
        base.UpdateAnimationParams();
        
        float battleAnimSpeedMultiplier = Enemy.battleMoveSpeed / Enemy.moveSpeed;
        Animator.SetFloat(_battleAnimSpeedMultiplier, battleAnimSpeedMultiplier);
        Animator.SetFloat(_moveAnimSpeedMultiplier, Enemy.moveAnimSpeedMultiplier);
        Animator.SetFloat(_velocityX, Rb.linearVelocity.x);
    }
}