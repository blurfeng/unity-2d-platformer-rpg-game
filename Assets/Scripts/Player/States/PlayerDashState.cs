using UnityEngine;

public class DashState : PlayerState
{
    private float _originalGravityScale;
    private int _dashDir;
    
    public DashState(Player player, StateMachine stateMachine) 
        : base(player, stateMachine, "Dash", "dash")
    {
    }

    public override void Enter()
    {
        base.Enter();

        _dashDir = MoveInput.x != 0 ? (int)Mathf.Sign(MoveInput.x) : Player.FacingDir;
        stateTimer = Player.dashDuration;
        _originalGravityScale = Rb.gravityScale;
        Rb.gravityScale = 0f;
    }
    
    public override void Exit()
    {
        base.Exit();

        Player.ClearVelocity();
        Rb.gravityScale = _originalGravityScale;
    }

    public override void Update(float deltaTime)
    {
        base.Update(deltaTime);
        
        Player.SetVelocityX(_dashDir * Player.dashSpeed);

        CancelDashCheck();
    }

    private void CancelDashCheck()
    {
        bool cancel = stateTimer <= 0f || Player.IsWallDetected;
        
        if (cancel)
        {
            if (Player.IsGrounded)
            {
                // 在地面时，直接切换到Idle。
                Player.ChangeToIdleState();
            }
            else
            {
                // 在空中时，尝试切换到抓墙壁滑动，如果实际上没有墙壁会直接切换到Fall。
                Player.ChangeToWallSlideState();
            }
        }
    }
}