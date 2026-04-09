public abstract class EnemyGroundedState : EnemyState
{
    protected EnemyGroundedState(Enemy enemy, StateMachine stateMachine, string stateName, string animBoolName) 
        : base(enemy, stateMachine, stateName, animBoolName)
    {
    }

    public override void Update(float deltaTime)
    {
        base.Update(deltaTime);

        if (Enemy.PlayerDetected())
        {
            Enemy.ChangeToBattleState();
        }
    }
}