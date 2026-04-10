
public class EnemySkeleton : Enemy
{
    public override void HandleCounter()
    {
        base.HandleCounter();
        
        if (!CanBeCountered)
            return;
        
        ChangeToStunnedState();
    }
}