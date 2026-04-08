using UnityEngine;

public abstract class Enemy : Entity
{
    public EnemyIdleState IdleState { get; private set; }
    public void ChangeToIdleState() => StateMachine?.ChangeState(IdleState);
    public EnemyMoveState MoveState { get; private set; }
    public void ChangeToMoveState() => StateMachine?.ChangeState(MoveState);

    [Header("Movement")] 
    public float idleTime = 2f;
    public float moveSpeed = 1.4f;
    public float moveAnimSpeedMultiplier = 1f;

    protected override void Awake()
    {
        base.Awake();
        
        IdleState = new EnemyIdleState(this, StateMachine);
        MoveState = new EnemyMoveState(this, StateMachine);
    }

    protected override void Start()
    {
        base.Start();
        
        StateMachine.Initialize(IdleState);
    }
}