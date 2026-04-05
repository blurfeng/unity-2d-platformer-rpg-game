public class WallSlideState : EntityState
{
    public WallSlideState(StateMachine stateMachine) : base(stateMachine, "WallSlide", "wallSlide")
    {
    }

    public override void Update(float deltaTime)
    {
        base.Update(deltaTime);
        
        HandleWallSlide();
        
        if (PlayerInput.Player.Jump.WasPressedThisDynamicUpdate())
        {
            Player.ChangeToWallJumpState();
        }
        else if (Player.IsGrounded)
        {
            Player.ChangeToIdleState();
            Player.Flip();
        }
        else if (!Player.IsWallDetected)
        {
            Player.ChangeToFallState();
        }
    }

    private void HandleWallSlide()
    {
        if (MoveInput.y < 0)
        {
            Player.SetVelocity(MoveInput.x, Rb.linearVelocity.y);
        }
        else
        {
            Player.SetVelocity(MoveInput.x, Rb.linearVelocity.y * Player.wallSliderMultiplier);
        }
    }
        
}