using UnityEngine;

public class Player : MonoBehaviour
{
    public StateMachine StateMachine { get; private set; }

    private EntityState _idleState;
    
    private void Awake()
    {
        StateMachine = new StateMachine();
        _idleState = new EntityState(StateMachine, "Idle State");
    }

    private void Start()
    {
        StateMachine.Initialize(_idleState);
    }

    private void Update()
    {
        StateMachine.Update(Time.deltaTime);
    }
}
