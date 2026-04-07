using UnityEngine;

public class DashState : EntityState
{
    private float _originalGravityScale;
    private int _dashDir;
    
    public DashState(StateMachine stateMachine) : base(stateMachine, "Dash", "dash")
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
                Player.ChangeToIdleState();
            else
                Player.ChangeToFallState();
        }
    }
}