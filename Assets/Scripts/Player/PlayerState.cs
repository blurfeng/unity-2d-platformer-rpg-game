using UnityEngine;

/// <summary>
/// 实体状态的基类，定义了状态的基本结构和行为。
/// </summary>
public abstract class PlayerState : EntityState
{
    protected Player Player { get; private set; }
    
    protected PlayerInputSet Input { get; private set; }
    
    protected Vector2 MoveInput => Player?.MoveInput ?? Vector2.zero;

    protected PlayerState(Player player, StateMachine stateMachine, string stateName, string animBoolName) 
        : base(stateMachine, stateName, animBoolName)
    {
        Player = player;
        Animator = Player.Animator;
        Rb = Player.Rb;
        Input = Player.PlayerInput;
    }

    public override void Update(float deltaTime)
    {
        base.Update(deltaTime);
        
        if (Input.Player.Dash.WasPressedThisFrame() && CanDash())
        {
            Player.ChangeToDashState();
        }
    }


    private bool CanDash()
    {
        if (Player.IsWallDetected)
            return false;
        
        if (StateMachine.CurrentState == Player.DashState)
            return false;
        
        return true;
    }
}
