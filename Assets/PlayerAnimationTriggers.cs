using System;
using UnityEngine;

public class PlayerAnimationTriggers : MonoBehaviour
{
    private Player _player;

    private void Awake()
    {
        _player = GetComponentInParent<Player>();
    }

    public void CurrentStateTrigger()
    {
        _player.CallAnimationTrigger();
    }
}