using UnityEngine;

public class PlayerCombat : EntityCombat
{
    [Header("Counter Attack")] 
    [SerializeField] private float counterRecovery = 0.25f;
    
    public bool CounterAttackPerformed()
    {
        bool performed = false;
        foreach (var co in GetDetectedColliders())
        {
            ICounterable counterable = co.GetComponent<ICounterable>();
            if (counterable == null) continue;
            
            if (counterable.CanBeCountered)
            {
                counterable.HandleCounter();
                performed = true;
            }
        }
        
        return performed;
    }
    
    public float GetCounterRecoveryDuration()
    {
        return counterRecovery;
    } 
}