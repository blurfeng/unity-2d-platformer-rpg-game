using UnityEngine;

public class EntityState
{
    protected StateMachine stateMachine;
    protected string stateName;
    
    public EntityState(StateMachine stateMachine, string stateName)
    {
        this.stateMachine = stateMachine;
        this.stateName = stateName;
    }

    public virtual void Enter()
    {
        // 当状态改变时，新状态进入时执行的逻辑
        Debug.Log($"Entering state: {stateName}.");
    }
    
    public virtual void Update(float deltaTime)
    {
        // 在状态更新时执行的逻辑
        Debug.Log($"Updating state: {stateName}.");
    }

    public virtual void Exit()
    {
        // 当状态改变时，旧状态退出时执行的逻辑
        Debug.Log($"Exiting state: {stateName}.");
    }
}
