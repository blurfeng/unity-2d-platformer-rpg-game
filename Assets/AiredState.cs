public abstract class AiredState : GroundedState
{
    public AiredState(StateMachine stateMachine, string stateName, string animBoolName) : base(stateMachine, stateName, animBoolName)
    {
    }

    public override void Update(float deltaTime)
    {
        base.Update(deltaTime);

        if (Player.HasMoveInputX)
        {
            Player.SetVelocityX(Player.MoveInput.x * (Player.moveSpeed * Player.airedMoveMultiplier));
        }
        

    }
        
}