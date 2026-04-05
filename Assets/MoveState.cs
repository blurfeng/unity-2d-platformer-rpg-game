using UnityEngine;

/// <summary>
/// 代表实体处于空闲状态的类，继承自EntityState。
/// </summary>
public class MoveState : EntityState
{
    public MoveState(StateMachine stateMachine) : base(stateMachine, "Move", "move")
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
        
        if (!Player.HasMoveInputX)
            Player.ChangeToIdleState();
    }
}