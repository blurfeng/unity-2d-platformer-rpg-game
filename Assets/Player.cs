using UnityEngine;

public class Player : MonoBehaviour
{
    public StateMachine StateMachine { get; private set; }

    private EntityState _idleState;
    public void ChangeToIdleState() => StateMachine?.ChangeState(_idleState);
    private EntityState _moveState;
    public void ChangeToMoveState() => StateMachine?.ChangeState(_moveState);
    
    private void Awake()
    {
        StateMachine = new StateMachine();
        _idleState = new IdleState(StateMachine);
        _moveState = new MoveState(StateMachine);
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
