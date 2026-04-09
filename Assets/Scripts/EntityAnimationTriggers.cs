using System;
using UnityEngine;

public class EntityAnimationTriggers : MonoBehaviour
{
    private Entity _entity;

    private void Awake()
    {
        _entity = GetComponentInParent<Entity>();
    }

    public void CurrentStateTrigger()
    {
        _entity.CurrentStateAnimationTrigger();
    }
}