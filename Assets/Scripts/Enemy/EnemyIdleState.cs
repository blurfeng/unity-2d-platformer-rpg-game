public class EnemyIdleState : EnemyGroundedState
{
    public EnemyIdleState(Enemy enemy, StateMachine stateMachine) 
        : base(enemy, stateMachine, "Idle", "idle")
    {
    }

    public override void Enter()
    {
        base.Enter();
        
        stateTimer = Enemy.idleTime;
    }

    public override void Update(float deltaTime)
    {
        base.Update(deltaTime);

        if (stateTimer <= 0)
        {
            Enemy.ChangeToMoveState();
        }
    }
}