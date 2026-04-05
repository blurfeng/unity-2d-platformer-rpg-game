using UnityEngine;

public abstract class GroundedState : EntityState
{
    protected GroundedState(StateMachine stateMachine, string stateName, string animBoolName) : base(stateMachine, stateName, animBoolName)
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
        
        if (PlayerInput.Player.Jump.WasPerformedThisFrame())
        {
            Player.ChangeToJumpState();
        }

        if (!Player.IsGrounded && Rb.linearVelocity.y < 0f)
        {
            Player.ChangeToFallState();
        }
    }
}