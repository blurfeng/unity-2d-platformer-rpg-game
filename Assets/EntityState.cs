using UnityEngine;

/// <summary>
/// 实体状态的基类，定义了状态的基本结构和行为。
/// </summary>
public abstract class EntityState
{
    protected StateMachine StateMachine { get; private set; }
    protected Player Player => StateMachine?.Player;
    protected Animator Animator => Player?.Animator;
    protected string StateName { get; private set; }
    protected string AnimBoolName { get; private set; }
    
    public EntityState(StateMachine stateMachine, string stateName, string animBoolName)
    {
        StateMachine = stateMachine;
        StateName = stateName;
        AnimBoolName = animBoolName;
    }

    public virtual void Enter()
    {
        // 当状态改变时，新状态进入时执行的逻辑
        Debug.Log($"Entering state: {StateName}.");
        
        Animator.SetBool(AnimBoolName, true);
    }
    
    public virtual void Exit()
    {
        // 当状态改变时，旧状态退出时执行的逻辑
        Debug.Log($"Exiting state: {StateName}.");
        
        Animator.SetBool(AnimBoolName, false);
    }
    
    public virtual void Update(float deltaTime)
    {
        // 在状态更新时执行的逻辑
        Debug.Log($"Updating state: {StateName}.");
    }
}
