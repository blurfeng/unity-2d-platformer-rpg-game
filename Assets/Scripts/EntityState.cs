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
        triggerCalled = false;
        
        if (!string.IsNullOrEmpty(AnimBoolName))
            Animator.SetBool(AnimBoolName, true);
    }
    
    public virtual void Exit()
    {
        if (!string.IsNullOrEmpty(AnimBoolName))
            Animator.SetBool(AnimBoolName, false);
    }
    
    public virtual void Update(float deltaTime)
    {
        stateTimer -= deltaTime;
        UpdateAnimationParams();
    }

    public void AnimationTrigger()
    {
        triggerCalled = true;
    }

    public virtual void UpdateAnimationParams()
    {
        Animator.SetFloat(_velocityY, Rb.linearVelocity.y);
    }
}