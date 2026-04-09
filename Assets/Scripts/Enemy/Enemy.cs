using UnityEngine;

public abstract class Enemy : Entity
{
    public EnemyIdleState IdleState { get; private set; }
    public void ChangeToIdleState() => StateMachine?.ChangeState(IdleState);
    public EnemyMoveState MoveState { get; private set; }
    public void ChangeToMoveState() => StateMachine?.ChangeState(MoveState);
    public EnemyAttackState AttackState { get; private set; }
    public void ChangeToAttackState() => StateMachine?.ChangeState(AttackState);
    public EnemyBattleState BattleState { get; private set; }
    public void ChangeToBattleState() => StateMachine?.ChangeState(BattleState);

    [Header("Battle")] 
    public float battleMoveSpeed = 3f;
    public float attackDistance = 2f;
    public float battleTimeDuration = 5f;
    public float minRetreatDistance = 1f;
    public Vector2 retreatVelocity = new Vector2(5f, 3f);

    [Header("Movement")] 
    public float idleTime = 2f;
    public float moveSpeed = 1.4f;
    public float moveAnimSpeedMultiplier = 1f;
    
    [Header("Player detection")]
    [SerializeField] private LayerMask playerLayer;
    [SerializeField] private Transform playerCheck;
    [SerializeField] private float playerCheckDistance = 10f;

    protected override void Awake()
    {
        base.Awake();
        
        IdleState = new EnemyIdleState(this, StateMachine);
        MoveState = new EnemyMoveState(this, StateMachine);
        AttackState = new EnemyAttackState(this, StateMachine);
        BattleState = new EnemyBattleState(this, StateMachine);
    }

    protected override void Start()
    {
        base.Start();
        
        StateMachine.Initialize(IdleState);
    }

    public RaycastHit2D PlayerDetected()
    {
        var hit = Physics2D.Raycast(playerCheck.position, Vector2.right * FacingDir, playerCheckDistance, playerLayer | groundLayer);
        
        if (!hit.collider || hit.collider.gameObject.layer != LayerMask.NameToLayer("Player"))
            return default;

        return hit;
    }

    protected override void OnDrawGizmos()
    {
        base.OnDrawGizmos();
        
        if (playerCheck)
        {
            Gizmos.color = Color.yellow;
            Gizmos.DrawLine(playerCheck.position, new Vector3(playerCheck.position.x + FacingDir * playerCheckDistance, playerCheck.position.y));
            Gizmos.color = Color.blue;
            Gizmos.DrawLine(playerCheck.position, new Vector3(playerCheck.position.x + FacingDir * attackDistance, playerCheck.position.y));
            Gizmos.color = Color.green;
            Gizmos.DrawLine(playerCheck.position, new Vector3(playerCheck.position.x + FacingDir * minRetreatDistance, playerCheck.position.y));
        }
    }
}