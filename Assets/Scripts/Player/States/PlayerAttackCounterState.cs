using UnityEngine;

public class PlayerAttackCounterState : PlayerState
{
    private static readonly int _attackCounterPerformed = Animator.StringToHash("attackCounterPerformed");
    private readonly PlayerCombat _combat;
    private bool _countered;
    
    public PlayerAttackCounterState(Player player, StateMachine stateMachine) 
        : base(player, stateMachine, "AttackCounter", "attackCounter")
    {
        _combat = player.GetComponent<PlayerCombat>();
    }

    public override void Enter()
    {
        base.Enter();
        
        stateTimer = _combat.GetCounterRecoveryDuration();
        _countered = _combat.CounterAttackPerformed();
        Animator.SetBool(_attackCounterPerformed, _countered);
    }

    public override void Exit()
    {
        base.Exit();
        
        Animator.SetBool(_attackCounterPerformed, false);
    }

    public override void Update(float deltaTime)
    {
        base.Update(deltaTime);
        
        Player.ClearVelocityX();
        
        if (triggerCalled || (stateTimer <= 0f && !_countered))
            Player.ChangeToIdleState();
    }
}
