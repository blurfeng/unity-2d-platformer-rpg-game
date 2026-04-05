public class JumpState : AiredState
{
    public JumpState(StateMachine stateMachine) : base(stateMachine, "Jump", "jumpFall")
    {
    }

    public override void Enter()
    {
        base.Enter();
        
        Player.SetVelocityY(Player.jumpForce);
    }
    
    public override void Exit()
    {
        base.Exit();
    }

    public override void Update(float deltaTime)
    {
        base.Update(deltaTime);
        
        if (Rb.linearVelocity.y < 0f)
        {
            Player.ChangeToFallState();
        }
    }
        
}