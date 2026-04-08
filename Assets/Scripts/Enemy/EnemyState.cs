using UnityEngine;

public abstract class EnemyState : EntityState
{
    private static readonly int _moveAnimSpeedMultiplier = Animator.StringToHash("moveAnimSpeedMultiplier");
    protected Enemy Enemy { get; private set; }
    
    public EnemyState(Enemy enemy, StateMachine stateMachine, string stateName, string animBoolName) 
        : base(stateMachine, stateName, animBoolName)
    {
        Enemy = enemy;
        Animator = Enemy.Animator;
        Rb = Enemy.Rb;
    }

    public override void Update(float deltaTime)
    {
        base.Update(deltaTime);
        
        Animator.SetFloat(_moveAnimSpeedMultiplier, Enemy.moveAnimSpeedMultiplier);
    }
}