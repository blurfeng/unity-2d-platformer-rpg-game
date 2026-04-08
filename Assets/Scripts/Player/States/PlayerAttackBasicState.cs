using UnityEngine;

public class PlayerAttackBasicState : PlayerState
{
    private static readonly int _attackBasicIndex = Animator.StringToHash("attackBasicIndex");
    
    private float _attackVelocityTimer;
    private float _lastTimeAttacked;
    
    private bool _comboAttackQueued;
    private int _attackDir;
    private int _comboIndex = 1;
    private const int ComboIndexStart = 1;
    private int _comboIndexLimit = 3;
    
    public PlayerAttackBasicState(Player player, StateMachine stateMachine) 
        : base(player, stateMachine, "AttackBasic", "attackBasic")
    {
    }

    public override void Enter()
    {
        base.Enter();
        
        if (_comboIndexLimit != Player.attackVelocities.Length)
        {
            _comboIndexLimit = Player.attackVelocities.Length;
            Debug.LogWarning($"ComboIndexLimit ({_comboIndexLimit}) must match the length of Player.attackVelocities ({Player.attackVelocities.Length}).");
        }
        _comboAttackQueued = false;
        ResetComboIndex();
        
        // 根据玩家输入判断当前攻击方向。
        _attackDir = MoveInput.x != 0 ? (int)Mathf.Sign(MoveInput.x) : Player.FacingDir;
        
        // 设置攻击组合动画下标值。
        Animator.SetInteger(_attackBasicIndex, _comboIndex);
        ApplyAttackVelocity();
    }

    public override void Exit()
    {
        base.Exit();

        _comboIndex++;
        _lastTimeAttacked = Time.time;
    }

    public override void Update(float deltaTime)
    {
        base.Update(deltaTime);
        HandleAttackVelocity(deltaTime);
        
        if (Input.Player.Attack.WasPerformedThisFrame())
            QueueNextAttack();

        if (triggerCalled)
            HandleStateExit();
    }

    private void HandleStateExit()
    {
        if (_comboAttackQueued)
        {
            Animator.SetBool(AnimBoolName, false);
            Player.EnterAttackStateWithDelay();
        }
        else
            Player.ChangeToIdleState();
    }

    public void QueueNextAttack()
    {
        if (_comboIndex < _comboIndexLimit)
            _comboAttackQueued = true;
    }

    private void HandleAttackVelocity(float deltaTime)
    {
        _attackVelocityTimer -= deltaTime;
        
        if (_attackVelocityTimer < 0)
            Player.ClearVelocityX();
    }

    private void ApplyAttackVelocity()
    {
        _attackVelocityTimer = Player.attackVelocityDuration;
        Vector2 attackVelocity = Player.attackVelocities[_comboIndex - 1];
        Player.SetVelocity(attackVelocity.x * _attackDir, attackVelocity.y);
    }
    
    private void ResetComboIndex()
    {
        if (Time.time >  _lastTimeAttacked + Player.attackComboResetTime 
            || _comboIndex > _comboIndexLimit)
            _comboIndex = ComboIndexStart;
    }
}