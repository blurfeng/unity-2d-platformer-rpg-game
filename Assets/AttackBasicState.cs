public class AttackBasicState : EntityState
{
    private float _attackVelocityTimer;
    
    public AttackBasicState(StateMachine stateMachine) : base(stateMachine, "AttackBasic", "attackBasic")
    {
    }

    public override void Enter()
    {
        base.Enter();
        GenerateAttackVelocity();
    }

    public override void Update(float deltaTime)
    {
        base.Update(deltaTime);
        
        if (triggerCalled)
            Player.ChangeToIdleState();
        
        HandleAttackVelocity(deltaTime);
    }

    private void HandleAttackVelocity(float deltaTime)
    {
        _attackVelocityTimer -= deltaTime;
        
        if (_attackVelocityTimer < 0)
            Player.ClearVelocityX();
    }

    private void GenerateAttackVelocity()
    {
        _attackVelocityTimer = Player.attackVelocityDuration;
        Player.SetVelocityX(Player.attackVelocity.x * Player.FacingDir);
    }
}