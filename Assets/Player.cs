using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class Player : MonoBehaviour
{
    public Animator Animator { get; private set; }
    public Rigidbody2D Rb { get; private set; }
    public PlayerInputSet PlayerInput { get; private set; }
    public StateMachine StateMachine { get; private set; }

    public IdleState IdleState { get; private set; }
    public void ChangeToIdleState() => StateMachine?.ChangeState(IdleState);
    public MoveState MoveState { get; private set; }
    public void ChangeToMoveState() => StateMachine?.ChangeState(MoveState);
    public JumpState JumpState { get; private set; }
    public void ChangeToJumpState() => StateMachine?.ChangeState(JumpState);
    public FallState FallState { get; private set; }
    public void ChangeToFallState() => StateMachine?.ChangeState(FallState);
    public WallSlideState WallSlideState { get; private set; }
    public void ChangeToWallSlideState() => StateMachine?.ChangeState(WallSlideState);
    public WallJumpState WallJumpState { get; private set; }
    public void ChangeToWallJumpState() => StateMachine?.ChangeState(WallJumpState);
    public DashState DashState { get; private set; }
    public void ChangeToDashState() => StateMachine?.ChangeState(DashState);

    public Vector2 MoveInput  { get; private set; }
    public bool HasMoveInput => MoveInput != Vector2.zero;
    public bool HasMoveInputX => MoveInput.x != 0;
    public bool HasMoveInputY => MoveInput.y != 0;
    public bool JumpPressed { get; private set; }

    [Header("Movement")] 
    public float moveSpeed = 8f;
    public bool facingRight = true;
    public float FacingDir { get; private set; } = 1f;
    public float jumpForce = 12f;
    public Vector2 wallJumpForce = new Vector2(6f, 12f);
    [Range(0, 1)]
    public float airedMoveMultiplier = 0.7f;
    [Range(0, 1)]
    public float wallSliderMultiplier = 0.7f;
    [Space]
    public float dashDuration = 0.25f;

    public float dashSpeed = 20f;

    [Header("Collision detection")]
    [SerializeField]
    private float groundCheckDistance = 1.1f;
    [SerializeField]
    private float wallCheckDistance = 0.4f;
    [SerializeField]
    private LayerMask groundLayer;
    
    /// <summary>
    /// 是否接触地面，基于groundCheckDistance和groundLayer进行检测。
    /// </summary>
    public bool IsGrounded {get; private set;}
    
    /// <summary>
    /// 是否接触墙壁，基于wallCheckDistance和groundLayer进行检测。
    /// </summary>
    public bool IsWallDetected {get; private set;}

    private void Awake()
    {
        Animator = GetComponentInChildren<Animator>();
        Rb = GetComponent<Rigidbody2D>();
        
        StateMachine = new StateMachine();
        PlayerInput = new PlayerInputSet();
        
        IdleState = new IdleState(StateMachine);
        MoveState = new MoveState(StateMachine);
        JumpState = new JumpState(StateMachine);
        FallState = new FallState(StateMachine);
        WallSlideState = new WallSlideState(StateMachine);
        WallJumpState = new WallJumpState(StateMachine);
        DashState = new DashState(StateMachine);
    }

    private void OnEnable()
    {
        PlayerInput.Enable();
        
        PlayerInput.Player.Movement.performed += ctx => MoveInput = ctx.ReadValue<Vector2>();
        PlayerInput.Player.Movement.canceled += ctx => MoveInput = Vector2.zero;
        
        PlayerInput.Player.Jump.performed += ctx => JumpPressed = true;
        PlayerInput.Player.Jump.canceled += ctx => JumpPressed = false;
    }

    private void OnDisable()
    {
        PlayerInput.Disable();
    }

    private void Start()
    {
        StateMachine.Initialize(this, IdleState);
    }

    private void Update()
    {
        HandleCollisionDetected();
        StateMachine.Update(Time.deltaTime);
    }
    
    public void SetVelocity(float velocityX, float velocityY)
    {
        Rb.linearVelocity = new  Vector2(velocityX, velocityY);
        HandleFlip(velocityX);
    }
    
    public void SetVelocityX(float velocityX)
    {
        Rb.linearVelocity = new  Vector2(velocityX, Rb.linearVelocity.y);
        HandleFlip(velocityX);
    }

    public void SetVelocityY(float velocityY)
    {
        Rb.linearVelocity = new  Vector2(Rb.linearVelocity.x, velocityY);
    }

    public void ClearVelocityX()
    {
        Rb.linearVelocity = new  Vector2(0f, Rb.linearVelocity.y);
    }
    
    public void ClearVelocityY()
    {
        Rb.linearVelocity = new  Vector2(Rb.linearVelocity.x, 0f);
    }
    
    public void ClearVelocity()
    {
        Rb.linearVelocity = Vector2.zero;
    }

    private void HandleFlip(float velocityX)
    {
        if (facingRight && velocityX < 0)
        {
            Flip();
        }
        else if (!facingRight && velocityX > 0)
        {
            Flip();
        }
    }

    public void Flip()
    {
        transform.Rotate(0f, 180f, 0f);
        facingRight = !facingRight;
        FacingDir = facingRight ? 1f : -1f;
    }

    private void HandleCollisionDetected()
    {
        IsGrounded = Physics2D.Raycast(transform.position, Vector2.down, groundCheckDistance, groundLayer);
        IsWallDetected = Physics2D.Raycast(transform.position, new Vector2(FacingDir, 0f), wallCheckDistance, groundLayer);
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawLine(transform.position, transform.position + new Vector3(0f, -groundCheckDistance, 0f));
        Gizmos.DrawLine(transform.position, transform.position + new  Vector3(FacingDir * wallCheckDistance, 0f, 0f));
    }
}
