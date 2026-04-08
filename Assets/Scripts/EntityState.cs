using UnityEngine;

public abstract class EntityState
{
    private static readonly int _velocityY = Animator.StringToHash("velocityY");
    protected StateMachine StateMachine { get; private set; }

    protected string StateName { get; private set; }
    protected string AnimBoolName { get; private set; }
    public Animator Animator { get; protected set; }
    public Rigidbody2D Rb { get; protected set; }

    protected float stateTimer;
    protected bool triggerCalled;

    protected EntityState(StateMachine stateMachine, string stateName, string animBoolName)
    {
        StateMachine = stateMachine;
        StateName = stateName;
        AnimBoolName = animBoolName;
    }
    
    public virtual void Enter()
    {
        // 当状态改变时，新状态进入时执行的逻辑
        // Debug.Log($"Entering state: {StateName}.");
        triggerCalled = false;
        
        Animator.SetBool(AnimBoolName, true);
    }
    
    public virtual void Exit()
    {
        // 当状态改变时，旧状态退出时执行的逻辑
        // Debug.Log($"Exiting state: {StateName}.");
        
        Animator.SetBool(AnimBoolName, false);
    }
    
    public virtual void Update(float deltaTime)
    {
        // 在状态更新时执行的逻辑
        // Debug.Log($"Updating state: {StateName}.");
        Animator.SetFloat(_velocityY, Rb.linearVelocity.y);
        
        stateTimer -= deltaTime;
    }

    public void CallAnimationTrigger()
    {
        triggerCalled = true;
    }
}