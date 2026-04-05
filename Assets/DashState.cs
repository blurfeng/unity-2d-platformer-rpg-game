using UnityEngine;

public class DashState : EntityState
{
    private float _originalGravityScale;
    
    public DashState(StateMachine stateMachine) : base(stateMachine, "Dash", "dash")
    {
    }

    public override void Enter()
    {
        base.Enter();

        stateTimer = Player.dashDuration;
        _originalGravityScale = Rb.gravityScale;
        Rb.gravityScale = 0f;
        
        Debug.Log("Entered Dash State");
    }
    
    public override void Exit()
    {
        base.Exit();

        Player.ClearVelocity();
        Rb.gravityScale = _originalGravityScale;
        
        Debug.Log("Exited Dash State");
    }

    public override void Update(float deltaTime)
    {
        base.Update(deltaTime);
        
        Player.SetVelocityX(Player.FacingDir * Player.dashSpeed);

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