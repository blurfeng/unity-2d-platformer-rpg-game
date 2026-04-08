public class PlayerWallJumpState : PlayerState
{
    public PlayerWallJumpState(Player player, StateMachine stateMachine) 
        : base(player, stateMachine, "WallJump", "jumpFall")
    {
    }

    public override void Enter()
    {
        base.Enter();
        
        Player.SetVelocity(Player.wallJumpForce.x * -Player.FacingDir, Player.wallJumpForce.y);
    }

    public override void Update(float deltaTime)
    {
        base.Update(deltaTime);
        
        if (Rb.linearVelocity.y < 0f)
            Player.ChangeToFallState();
        else if (Player.IsWallDetected)
            Player.ChangeToWallSlideState();
    }
}