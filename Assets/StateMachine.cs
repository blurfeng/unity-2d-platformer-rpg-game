using UnityEngine;

public class StateMachine
{
    /// <summary>
    /// 当前状态。
    /// </summary>
    public EntityState CurrentState { get; private set; }
    
    public void Initialize(EntityState state)
    {
        SetState(state);
    }

    public void Update(float deltaTime)
    {
        CurrentState?.Update(deltaTime);
    }

    private void SetState(EntityState state)
    {
        if (CurrentState != null)
        {
            CurrentState.Exit();
        }
        
        CurrentState = state;
        CurrentState.Enter();
    }
}
