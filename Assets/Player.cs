using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class Player : MonoBehaviour
{
    public Animator Animator { get; private set; }
    public Rigidbody2D Rb { get; private set; }
    public PlayerInputSet PlayerInput { get; private set; }
    public StateMachine StateMachine { get; private set; }

    private IdleState _idleState;
    public void ChangeToIdleState() => StateMachine?.ChangeState(_idleState);
    private MoveState _moveState;
    public void ChangeToMoveState() => StateMachine?.ChangeState(_moveState);
    private JumpState _jumpState;
    public void ChangeToJumpState() => StateMachine?.ChangeState(_jumpState);
    private FallState _fallState;
    public void ChangeToFallState() => StateMachine?.ChangeState(_fallState);

    public Vector2 MoveInput  { get; private set; }
    public bool HasMoveInput => MoveInput != Vector2.zero;
    public bool HasMoveInputX => MoveInput.x != 0;
    public bool HasMoveInputY => MoveInput.y != 0;
    public bool JumpPressed { get; private set; }

    [Header("Movement")] 
    public float moveSpeed = 8f;
    public bool facingRight = true;
    [Header("Jump")]
    public float jumpForce = 5f;
    [Range(0, 1)]
    public float airedMoveMultiplier = 0.7f;

    [Header("Collision detection")]
    [SerializeField]
    private float groundCheckDistance = 1.1f;
    [SerializeField]
    private LayerMask groundLayer;
    /// <summary>
    /// 是否接触地面，基于groundCheckDistance和groundLayer进行检测。
    /// </summary>
    public bool IsGrounded {get; private set;}

    private void Awake()
    {
        Animator = GetComponentInChildren<Animator>();
        Rb = GetComponent<Rigidbody2D>();
        
        StateMachine = new StateMachine();
        PlayerInput = new PlayerInputSet();
        
        _idleState = new IdleState(StateMachine);
        _moveState = new MoveState(StateMachine);
        _jumpState = new JumpState(StateMachine);
        _fallState = new FallState(StateMachine);
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
        StateMachine.Initialize(this, _idleState);
    }

    private void Update()
    {
        HandleCollisionDetected();
        StateMachine.Update(Time.deltaTime);
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

    private void Flip()
    {
        transform.Rotate(0f, 180f, 0f);
        facingRight = !facingRight;
    }

    private void HandleCollisionDetected()
    {
        IsGrounded = Physics2D.Raycast(transform.position, Vector2.down, groundCheckDistance, groundLayer);
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawLine(transform.position, transform.position + Vector3.down * groundCheckDistance);
    }
}
