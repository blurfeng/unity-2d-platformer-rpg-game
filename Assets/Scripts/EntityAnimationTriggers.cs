using System;
using UnityEngine;

public class EntityAnimationTriggers : MonoBehaviour
{
    private Entity _entity;
    private EntityCombat _entityCombat;

    private void Awake()
    {
        _entity = GetComponentInParent<Entity>();
        _entityCombat = GetComponentInParent<EntityCombat>();
    }

    public void CurrentStateTrigger()
    {
        _entity.CurrentStateAnimationTrigger();
    }
    
    public void AttackTrigger()
    {
        _entityCombat.PerformAttack();
    }
}