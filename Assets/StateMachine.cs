using UnityEngine;

public class StateMachine
{
    /// <summary>
    /// 当前状态。
    /// </summary>
    public EntityState CurrentState { get; private set; }

    /// <summary>
    /// 状态机所属的玩家对象。
    /// </summary>
    public Player Player { get; private set; }
    
    public void Initialize(Player player, EntityState state)
    {
        Player = player;
        ChangeState(state);
    }

    public void Update(float deltaTime)
    {
        CurrentState?.Update(deltaTime);
    }

    public void ChangeState(EntityState state)
    {
        if (CurrentState != null)
        {
            CurrentState.Exit();
        }
        
        CurrentState = state;
        CurrentState.Enter();
    }
}
