using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class Player : MonoBehaviour
{
    public Animator Animator { get; private set; }
    public Rigidbody2D Rb { get; private set; }
    private PlayerInputSet _playerInput;
    public StateMachine StateMachine { get; private set; }

    private EntityState _idleState;
    public void ChangeToIdleState() => StateMachine?.ChangeState(_idleState);
    private EntityState _moveState;
    public void ChangeToMoveState() => StateMachine?.ChangeState(_moveState);

    public Vector2 MoveInput  { get; private set; }
    public bool HasMoveInput => MoveInput != Vector2.zero;
    public bool HasMoveInputX => MoveInput.x != 0;
    public bool HasMoveInputY => MoveInput.y != 0;

    [Header("Movement")] 
    public float moveSpeed = 8f;

    private void Awake()
    {
        Animator = GetComponentInChildren<Animator>();
        Rb = GetComponent<Rigidbody2D>();
        
        StateMachine = new StateMachine();
        _playerInput = new PlayerInputSet();
        
        _idleState = new IdleState(StateMachine);
        _moveState = new MoveState(StateMachine);
    }

    private void OnEnable()
    {
        _playerInput.Enable();
        
        _playerInput.Player.Movement.performed += ctx => MoveInput = ctx.ReadValue<Vector2>();
        _playerInput.Player.Movement.canceled += ctx => MoveInput = Vector2.zero;
    }

    private void OnDisable()
    {
        _playerInput.Disable();
    }

    private void Start()
    {
        StateMachine.Initialize(this, _idleState);
    }

    private void Update()
    {
        StateMachine.Update(Time.deltaTime);
    }
    
    public void SetVelocityX(float x)
    {
        Rb.linearVelocity = new  Vector2(x, Rb.linearVelocity.y);
    }
}
