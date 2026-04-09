public class PlayerDeadState : PlayerState
{
    public PlayerDeadState(Player player, StateMachine stateMachine) 
        : base(player, stateMachine, "Dead", "dead")
    {
    }

    public override void Enter()
    {
        base.Enter();
        
        Input.Disable();
        Rb.simulated = false;
    }
}