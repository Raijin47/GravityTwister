using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimation : MonoBehaviour
{
    private Animator _animator;
    private List<Rigidbody> _rigidbodies;

    private readonly string KeyRolling = "IsJump";
    private readonly string KeyInGame = "InGame";

    private bool IsAlive
    {
        set
        {
            foreach (Rigidbody rigidbody in _rigidbodies) rigidbody.isKinematic = value;
            _animator.enabled = value;
        }
    }

    private void Start()
    {
        _rigidbodies = new List<Rigidbody>(transform.GetChild(1).GetComponentsInChildren<Rigidbody>());
        _animator = GetComponentInChildren<Animator>();

        IsAlive = true;
        Game.Action.OnEnter += Action_OnEnter;
        Game.Action.OnExit += Action_OnExit;
        Game.Locator.Player.OnChangeGravity += Player_OnChangeGravity;
        Game.Action.OnLose += Action_OnLose;
    }

    private void Action_OnLose() => IsAlive = false;
    private void Player_OnChangeGravity() => _animator.SetTrigger(KeyRolling);
    private void Action_OnEnter() => _animator.SetBool(KeyInGame, true);
    private void Action_OnExit()
    {
        _animator.SetBool(KeyInGame, false);
        IsAlive = true;
    }
}