using UnityEngine;

public class StateMachine
{
    /// <summary>
    /// 当前状态。
    /// </summary>
    public EntityState CurrentState { get; private set; }
    
    public bool canChangeState;
    
    public void Initialize(EntityState state)
    {
        canChangeState = true;
        ChangeState(state);
    }

    public void Update(float deltaTime)
    {
        CurrentState?.Update(deltaTime);
    }

    public void ChangeState(EntityState state)
    {
        if (!canChangeState)
            return;
        
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
    public bool CheckCurrentState(EntityState state)
    {
        return CurrentState == state;
    }
    
    /// <summary>
    /// 禁止状态机切换状态。
    /// </summary>
    public void SwitchOffStateMachine() => canChangeState = false;
}
