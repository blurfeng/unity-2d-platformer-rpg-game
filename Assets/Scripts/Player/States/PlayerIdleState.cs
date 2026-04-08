using UnityEngine;

/// <summary>
/// 代表实体处于空闲状态的类，继承自EntityState。
/// </summary>
public class IdleState : PlayerGroundedState
{
    public IdleState(Player player, StateMachine stateMachine) 
        : base(player, stateMachine, "Idle", "idle")
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

        // 当角色在对着墙壁时，不能往墙壁移动。
        if (Mathf.Approximately(MoveInput.x, Player.FacingDir)&& Player.IsWallDetected)
            return;
        
        if (Player.HasMoveInputX)
            Player.ChangeToMoveState();
    }
}