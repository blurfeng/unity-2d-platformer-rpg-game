public class EnemyAnimationTriggers : EntityAnimationTriggers
{
    private Enemy _enemy;
    private EnemyVFX _enemyVFX;

    protected override void Awake()
    {
        base.Awake();
        
        _enemy = GetComponentInParent<Enemy>();
        _enemyVFX = GetComponentInParent<EnemyVFX>();
    }

    public void EnableCounterWindow()
    {
        _enemy.EnableCounterWindow(true);
        _enemyVFX.EnableAttackAlert(true);
    }

    public void DisableCounterWindow()
    {
        _enemy.EnableCounterWindow(false);
        _enemyVFX.EnableAttackAlert(false);
    }
}