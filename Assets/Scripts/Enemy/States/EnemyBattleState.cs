using UnityEngine;

public class EnemyBattleState : EnemyState
{
    private Transform _playerTransform;
    private float _lastTimeWatInBattle;
    
    public EnemyBattleState(Enemy enemy, StateMachine stateMachine) 
        : base(enemy, stateMachine, "Battle", "battle")
    {
    }

    public override void Enter()
    {
        base.Enter();
        
        UpdateBattleTimer();
        
        if (!_playerTransform)
            _playerTransform = Enemy.GetPlayerReference(out bool isDetected);

        if (ShouldRetreat())
        {
            float direction = DirectionToPlayer();
            Enemy.SetVelocity(Enemy.retreatVelocity.x * -direction, Enemy.retreatVelocity.y);
            Enemy.HandleFlip(direction);
        }
    }

    public override void Update(float deltaTime)
    {
        base.Update(deltaTime);

        // 当玩家离开战斗范围时，敌人会在一段时间后切换回空闲状态；如果玩家进入攻击范围，敌人会切换到攻击状态；否则，敌人会继续追踪玩家。
        if (BattleTimeIsOver())
        {
            Enemy.ChangeToIdleState();
        }

        // 如果玩家存在，敌人会持续追踪玩家，并在进入攻击范围时切换到攻击状态；如果玩家不存在，敌人会切换回空闲状态。
        if (Enemy.GetPlayerReference(out bool isDetected))
        {
            // 如果玩家被检测到，更新战斗计时器。
            // 这样当玩家离开视线一段时间后，敌人会切换回空闲状态。
            if (isDetected)
                UpdateBattleTimer();
            
            // 当玩家进入攻击范围时，敌人会切换到攻击状态。
            if (WithinAttackRange())
            {
                Enemy.ChangeToAttackState();
            }
            // 持续追踪玩家。
            else
            {
                Enemy.SetVelocityX(Enemy.battleMoveSpeed * DirectionToPlayer());
            }
        }
        else
        {
            Enemy.ChangeToIdleState();
        }
    }

    private void UpdateBattleTimer() => _lastTimeWatInBattle = Time.time;

    private bool BattleTimeIsOver() => Time.time > _lastTimeWatInBattle + Enemy.battleTimeDuration;

    /// <summary>
    /// 在战斗状态中，敌人会持续追踪玩家，并在进入攻击范围时切换到攻击状态。
    /// </summary>
    /// <returns></returns>
    private bool WithinAttackRange() => DistanceToPlayer() < Enemy.attackDistance;
    
    /// <summary>
    /// 在战斗状态中，如果玩家距离敌人过近，敌人会选择后退以保持一定的距离。
    /// </summary>
    /// <returns></returns>
    private bool ShouldRetreat() => DistanceToPlayer() < Enemy.minRetreatDistance;

    /// <summary>
    /// 计算敌人与玩家之间的水平距离。
    /// </summary>
    /// <returns></returns>
    private float DistanceToPlayer()
    {
        if (!_playerTransform)
            return float.MaxValue;
        
        return Mathf.Abs(_playerTransform.position.x - Enemy.transform.position.x);
    }
    
    /// <summary>
    /// 计算敌人朝向玩家的方向，返回1表示朝右，-1表示朝左。
    /// </summary>
    /// <returns></returns>
    private int DirectionToPlayer()
    {
        if (!_playerTransform)
            return 0;
        
        return _playerTransform.position.x > Enemy.transform.position.x ? 1 : -1;
    }
}