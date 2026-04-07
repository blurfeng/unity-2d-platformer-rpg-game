public abstract class AiredState : EntityState
{
    public AiredState(StateMachine stateMachine, string stateName, string animBoolName) : base(stateMachine, stateName, animBoolName)
    {
    }

    public override void Update(float deltaTime)
    {
        base.Update(deltaTime);

        if (Player.HasMoveInputX)
        {
            Player.SetVelocityX(MoveInput.x * (Player.moveSpeed * Player.airedMoveMultiplier));
        }

        // if (Player.IsWallDetected)
        // {
        //     Player.ChangeToWallSlideState();
        // }
        
        if (Input.Player.Attack.WasPerformedThisFrame())
        {
            Player.ChangeToJumpAttackState();
        }
    }
        
}