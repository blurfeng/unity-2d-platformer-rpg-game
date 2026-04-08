public abstract class EnemyState : EntityState
{
    protected Enemy Enemy { get; private set; }
    
    public EnemyState(Enemy enemy, StateMachine stateMachine, string stateName, string animBoolName) 
        : base(stateMachine, stateName, animBoolName)
    {
        Enemy = enemy;
        Animator = Enemy.Animator;
        Rb = Enemy.Rb;
    }
}