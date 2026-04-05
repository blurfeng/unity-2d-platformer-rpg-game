using UnityEngine;

/// <summary>
/// 代表实体处于空闲状态的类，继承自EntityState。
/// </summary>
public class IdleState : GroundedState
{
    public IdleState(StateMachine stateMachine) : base(stateMachine, "Idle", "idle")
    {
    }

    public override void Enter()
    {
        base.Enter();

        Player.ClearVelocityX();
    }
    
    public override void Exit()
    {
        base.Exit();
    }

    public override void Update(float deltaTime)
    {
        base.Update(deltaTime);
        
        if (Player.HasMoveInputX)
            Player.ChangeToMoveState();
    }
}