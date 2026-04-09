public class EnemyMoveState : EnemyGroundedState
{
    public EnemyMoveState(Enemy enemy, StateMachine stateMachine) 
        : base(enemy, stateMachine, "Move", "move")
    {
    }

    public override void Enter()
    {
        base.Enter();
        
        if (!Enemy.IsGroundedForward || Enemy.IsWallDetected)
        {
            Enemy.Flip();
        }
    }

    public override void Update(float deltaTime)
    {
        base.Update(deltaTime);
        
        Enemy.SetVelocityX(Enemy.FacingDir * Enemy.moveSpeed);

        if (!Enemy.IsGroundedForward || Enemy.IsWallDetected)
        {
            Enemy.ChangeToIdleState();
        }
    }
}