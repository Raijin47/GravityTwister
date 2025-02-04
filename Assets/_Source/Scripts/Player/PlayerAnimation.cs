using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimation : MonoBehaviour
{
    private Animator _animator;
    private List<Rigidbody> _rigidbodies;

    private readonly string KeyChangeGravity = "IsGravity";
    private readonly string KeySlide = "IsSlide";
    private readonly string KeyInGame = "InGame";
    private readonly string KeyJump = "IsJump";

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
        Game.Locator.Player.OnSlide += Player_OnSlide;
        Game.Locator.Player.OnJump += Player_OnJump;
        Game.Action.OnLose += Action_OnLose;
    }

    private void Player_OnJump() => _animator.SetTrigger(KeyJump);
    private void Player_OnSlide() => _animator.SetTrigger(KeySlide);
    private void Action_OnLose() => IsAlive = false;
    private void Player_OnChangeGravity() => _animator.SetTrigger(KeyChangeGravity);
    private void Action_OnEnter() => _animator.SetBool(KeyInGame, true);
    private void Action_OnExit()
    {
        _animator.SetBool(KeyInGame, false);
        IsAlive = true;
    }
}