using UnityEngine;

public class JumpAttackState : PlayerState
{
    private bool _touchedGround;
    private static readonly int _jumpAttackTrigger = Animator.StringToHash("jumpAttackTrigger");

    public JumpAttackState(Player player, StateMachine stateMachine) 
        : base(player, stateMachine, "JumpAttack", "jumpAttack")
    {
    }

    public override void Enter()
    {
        base.Enter();
        
        _touchedGround = false;
        Player.SetVelocityX(Player.jumpAttackVelocity.x * Player.FacingDir);
    }

    public override void Update(float deltaTime)
    {
        base.Update(deltaTime);

        if (Player.IsGrounded && !_touchedGround)
        {
            _touchedGround = true;
            Animator.SetTrigger(_jumpAttackTrigger);
            Player.ClearVelocityX();
        }

        if (triggerCalled && Player.IsGrounded)
        {
            Player.ChangeToIdleState();
        }
    }
}