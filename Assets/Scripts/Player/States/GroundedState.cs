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
        
        if (Input.Player.Jump.WasPerformedThisFrame())
        {
            Player.ChangeToJumpState();
        }
        else if (!Player.IsGrounded && Rb.linearVelocity.y < 0f)
        {
            Player.ChangeToFallState();
        }
        else if (Input.Player.Attack.WasPerformedThisFrame())
        {
            Player.ChangeToAttackBasicState();
        }
    }
}