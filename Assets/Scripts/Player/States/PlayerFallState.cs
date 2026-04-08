public class PlayerFallState : PlayerAiredState
{
    public PlayerFallState(Player player, StateMachine stateMachine) 
        : base(player, stateMachine, "Fall", "jumpFall")
    {
    }

    public override void Enter()
    {
        base.Enter();
    }
    
    public override void Exit()
    {
        base.Exit();
    }

    public override void Update(float deltaTime)
    {
        base.Update(deltaTime);
        
        if (Player.IsGrounded)
        {
            Player.ChangeToIdleState();
        }
        else if (Player.IsWallDetected)
        {
            // 可能撞到墙壁，进入抓墙滑动状态。
            Player.ChangeToWallSlideState();
        }
    }
}