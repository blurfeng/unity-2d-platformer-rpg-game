using System;
using UnityEngine;

public class UIManiHealthBar : MonoBehaviour
{
    private Entity _entity;

    private void Awake()
    {
        _entity = GetComponentInParent<Entity>();
    }

    private void OnEnable()
    {
        _entity.OnFlipped += HandleOnFlipped;
    }

    private void OnDisable()
    {
        _entity.OnFlipped -= HandleOnFlipped;
    }

    private void HandleOnFlipped()
    {
        transform.rotation = Quaternion.identity;
    }
}