
public class EnemyAttackState : EnemyState
{
    public EnemyAttackState(Enemy enemy, StateMachine stateMachine) 
        : base(enemy, stateMachine, "Attack", "attack")
    {
    }

    public override void Enter()
    {
        base.Enter();
    }

    public override void Update(float deltaTime)
    {
        base.Update(deltaTime);

        if (triggerCalled)
        {
            Enemy.ChangeToBattleState();
        }
    }
}