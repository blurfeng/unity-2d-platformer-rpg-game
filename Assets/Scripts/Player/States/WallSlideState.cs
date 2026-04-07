using UnityEngine;

public class WallSlideState : EntityState
{
    public WallSlideState(StateMachine stateMachine) : base(stateMachine, "WallSlide", "wallSlide")
    {
    }

    public override void Update(float deltaTime)
    {
        base.Update(deltaTime);
        
        HandleWallSlide();
        
        if (Input.Player.Jump.WasPressedThisDynamicUpdate())
        {
            Player.ChangeToWallJumpState();
        }
        else if (!Player.IsWallDetected)
        {
            Player.ChangeToFallState();
        }
        else if (Player.IsGrounded)
        {
            Player.ChangeToIdleState();
            
            // 当角色没有输入时，抓墙滑动动画面向墙壁的反向，所以需要将角色对象转向。
            // 但当玩家输入方向和当前面向（朝向墙壁）一致时，不进行转向。避免动画的异常切换。
            if (!Mathf.Approximately(Player.FacingDir, MoveInput.x))
                Player.Flip();
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