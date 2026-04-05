using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class Player : MonoBehaviour
{
    [SerializeField]
    private Rigidbody2D rb;
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

    private void Awake()
    {
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
}
