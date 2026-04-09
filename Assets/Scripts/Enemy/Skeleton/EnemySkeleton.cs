public class EnemySkeleton : Enemy
{
    public override void HandleCounter()
    {
        base.HandleCounter();
        
        if (!canBeStunned)
            return;
        
        ChangeToStunnedState();
    }
}