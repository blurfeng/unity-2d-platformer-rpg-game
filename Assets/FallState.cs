public class FallState : AiredState
{
    public FallState(StateMachine stateMachine) : base(stateMachine, "Fall", "jumpFall")
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
        
        if (Player.IsGrounded)
        {
            Player.ChangeToIdleState();
        }
    }
}