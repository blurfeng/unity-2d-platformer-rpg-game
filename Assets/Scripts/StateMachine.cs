using UnityEngine;

public class StateMachine
{
    /// <summary>
    /// 当前状态。
    /// </summary>
    public EntityState CurrentState { get; private set; }
    
    public void Initialize(EntityState state)
    {
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
    
    /// <summary>
    /// 检查当前状态是否与指定状态相同。
    /// </summary>
    /// <param name="state"></param>
    /// <returns></returns>
    public bool CheckCurrentState(PlayerState state)
    {
        return CurrentState == state;
    }
}
