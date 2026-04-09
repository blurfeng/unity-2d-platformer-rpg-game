using System;
using System.Collections;
using UnityEngine;

public class Player : Entity
{
    public static event Action<Player> OnPlayerDeath;
    
    public PlayerInputSet PlayerInput { get; private set; }
    public PlayerIdleState IdleState { get; private set; }
    public void ChangeToIdleState() => StateMachine?.ChangeState(IdleState);
    public PlayerMoveState MoveState { get; private set; }
    public void ChangeToMoveState() => StateMachine?.ChangeState(MoveState);
    public PlayerJumpState JumpState { get; private set; }
    public void ChangeToJumpState() => StateMachine?.ChangeState(JumpState);
    public PlayerFallState FallState { get; private set; }
    public void ChangeToFallState() => StateMachine?.ChangeState(FallState);
    public PlayerWallSlideState WallSlideState { get; private set; }
    public void ChangeToWallSlideState() => StateMachine?.ChangeState(WallSlideState);
    public PlayerWallJumpState PlayerWallJumpState { get; private set; }
    public void ChangeToWallJumpState() => StateMachine?.ChangeState(PlayerWallJumpState);
    public PlayerDashState DashState { get; private set; }
    public void ChangeToDashState() => StateMachine?.ChangeState(DashState);
    public PlayerAttackBasicState AttackBasicState { get; private set; }
    public void ChangeToAttackBasicState() => StateMachine?.ChangeState(AttackBasicState);
    public PlayerJumpAttackState JumpAttackState { get; private set; }
    public void ChangeToJumpAttackState() => StateMachine?.ChangeState(JumpAttackState);
    public PlayerDeadState DeadState { get; private set; }
    public void ChangeToDeadState() => StateMachine?.ChangeState(DeadState);
    
    public Vector2 MoveInput  { get; private set; }
    public bool HasMoveInput => MoveInput != Vector2.zero;
    public bool HasMoveInputX => MoveInput.x != 0;
    public bool HasMoveInputY => MoveInput.y != 0;
    public bool JumpPressed { get; private set; }

    [Header("Attack")] 
    public Vector2[] attackVelocities = new Vector2[]
    {
        new Vector2(3f, 1.5f),
        new Vector2(1f, 2.5f),
        new Vector2(2.75f, 1.75f)
    };
    public Vector2 jumpAttackVelocity = new Vector2(5f, -6f);
    public float attackVelocityDuration = 0.1f;
    public float attackComboResetTime = 0.2f;
    private Coroutine _attackQueueCo;

    [Header("Movement")] 
    public float moveSpeed = 8f;
    public float jumpForce = 12f;
    public Vector2 wallJumpForce = new Vector2(6f, 12f);
    [Range(0, 1)]
    public float airedMoveMultiplier = 0.7f;
    [Range(0, 1)]
    public float wallSliderMultiplier = 0.7f;
    [Space]
    public float dashDuration = 0.25f;
    public float dashSpeed = 20f;
    
    public bool IsDead() => StateMachine.CheckCurrentState(DeadState);

    protected override void Awake()
    {
        base.Awake();
        
        PlayerInput = new PlayerInputSet();
        
        IdleState = new PlayerIdleState(this, StateMachine);
        MoveState = new PlayerMoveState(this, StateMachine);
        JumpState = new PlayerJumpState(this, StateMachine);
        FallState = new PlayerFallState(this, StateMachine);
        WallSlideState = new PlayerWallSlideState(this, StateMachine);
        PlayerWallJumpState = new PlayerWallJumpState(this, StateMachine);
        DashState = new PlayerDashState(this, StateMachine);
        AttackBasicState = new PlayerAttackBasicState(this, StateMachine);
        JumpAttackState = new PlayerJumpAttackState(this, StateMachine);
        DeadState = new PlayerDeadState(this, StateMachine);
    }

    protected override void Start()
    {
        base.Start();
        
        StateMachine.Initialize(IdleState);
    }

    protected override void OnEnable()
    {
        base.OnEnable();
        
        PlayerInput.Enable();
        
        PlayerInput.Player.Movement.performed += ctx => MoveInput = ctx.ReadValue<Vector2>();
        PlayerInput.Player.Movement.canceled += ctx => MoveInput = Vector2.zero;
        
        PlayerInput.Player.Jump.performed += ctx => JumpPressed = true;
        PlayerInput.Player.Jump.canceled += ctx => JumpPressed = false;
    }

    protected override void OnDisable()
    {
        base.OnDisable();
        
        PlayerInput.Disable();
    }

    public override void Death()
    {
        base.Death();
        
        ChangeToDeadState();
        
        OnPlayerDeath?.Invoke(this);
    }

    public void EnterAttackStateWithDelay()
    {
        if (_attackQueueCo != null)
        {
            StopCoroutine(_attackQueueCo);
        }
        _attackQueueCo = StartCoroutine(HandleCollisionDetectedCo());
    }

    private IEnumerator HandleCollisionDetectedCo()
    {
        yield return new WaitForEndOfFrame();
        StateMachine.ChangeState(AttackBasicState);
    }
}
