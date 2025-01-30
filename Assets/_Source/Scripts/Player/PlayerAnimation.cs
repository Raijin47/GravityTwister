using UnityEngine;

public class PlayerAnimation : MonoBehaviour
{
    private Animator _animator;

    private readonly string KeyRolling = "IsJump";

    private void Start()
    {
        _animator = GetComponent<Animator>();
        Game.Action.OnEnter += Action_OnEnter;
        Game.Action.OnExit += Action_OnExit;
        Game.Locator.Player.OnChangeGravity += Player_OnChangeGravity;
    }

    private void Player_OnChangeGravity() => _animator.SetTrigger(KeyRolling);
    private void Action_OnEnter() => _animator.SetBool("InGame", true);
    private void Action_OnExit() => _animator.SetBool("InGame", false);
}