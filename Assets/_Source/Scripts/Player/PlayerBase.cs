using DG.Tweening;
using System;
using UnityEngine;

public class PlayerBase : MonoBehaviour
{
    public event Action OnChangeGravity;
    public event Action OnSlide;
    public event Action OnJump;

    [SerializeField] private float _speed;
    [SerializeField] private float _requiredSpeed;
    [SerializeField] private Transform _anchor;
    [SerializeField] private Transform _view;

    private Rigidbody _rigidbody;
    private Sequence _sequence;
    private Tween _tween;

    private bool _isJump;
    private bool _canMove;
    private bool _isActive;

    private StatePosition _positionState;

    private readonly float MovementDuration = .5f;
    public float JumpDuration = .7f;
    public float FallingDuration = 0.1f;
    private readonly float ClipingDuration = 1f;


    private void Start()
    {
        _rigidbody = GetComponent<Rigidbody>();

        Game.Action.OnStart += Action_OnStart;
        Game.Action.OnExit += Action_OnExit;
        Game.Locator.Gravity.OnChangeCling += Gravity_OnChangeCling;
        Game.Locator.Input.OnLeft += Input_OnMove;
        Game.Locator.Input.OnJump += Input_OnJump;
        Game.Action.OnLose += Action_OnLose;
    }

    private void Action_OnLose()
    {
        _rigidbody.velocity = Vector3.zero;
        _canMove = false;
        _isActive = false;
    }

    private void Action_OnExit()
    {
        _canMove = false;
        _isActive = false;
        _anchor.localPosition = new Vector3(0, -1.5f, 0);
        _view.localPosition = Vector3.zero;
        _anchor.localRotation = Quaternion.Euler(Vector3.zero);
        _rigidbody.velocity = Vector3.zero;
        _rigidbody.MovePosition(new Vector3(0, 3, 0));
    }

    private void Action_OnStart()
    {
        _positionState = StatePosition.Middle;
        _canMove = true;
        _isActive = true;
    }

    private void Input_OnMove(bool isLeft)
    {
        if (!_canMove) return;

        switch (_positionState)
        {
            case StatePosition.Left:
                if (isLeft) break;
                else MoveMiddle();
                break;

            case StatePosition.Middle:
                if (isLeft) MoveLeft();
                else MoveRight();
                break;

            case StatePosition.Right:
                if (!isLeft) break;
                else MoveMiddle();
                break;
        }
    }

    private void Input_OnJump(bool isJump)
    {
        if (!_canMove) return;



        if (isJump) Jump();
        else Slide();
    }


    private void MoveLeft()
    {
        _positionState = StatePosition.Left;

        _tween?.Kill();
        _tween = _view.DOLocalMoveX(-1.5f, MovementDuration);
    }

    private void MoveMiddle()
    {
        _positionState = StatePosition.Middle;

        _tween?.Kill();
        _tween = _view.DOLocalMoveX(0, MovementDuration);
    }

    private void MoveRight()
    {
        _positionState = StatePosition.Right;

        _tween?.Kill();
        _tween = _view.DOLocalMoveX(1.5f, MovementDuration);
    }

    private void Jump()
    {
        if (_isJump) return;
        _isJump = true;

        Debug.Log("Jump");
        OnJump?.Invoke();

        _sequence?.Kill();
        _sequence = DOTween.Sequence();

        _sequence.
            Append(_view.DOLocalMoveY(2.5f, JumpDuration)).
            Append(_view.DOLocalMoveY(0, FallingDuration).OnComplete(() => _isJump = false));
    }

    private void Slide()
    {
        Debug.Log("Slide");
        OnSlide?.Invoke();
    }

    private void Gravity_OnChangeCling(Vector3 pos, Vector3 rot)
    {
        _canMove = false;

        _sequence?.Kill();

        _sequence = DOTween.Sequence();

        _positionState = StatePosition.Middle;
        OnChangeGravity?.Invoke();

        _sequence.
            Append(_anchor.DOLocalMove(pos, ClipingDuration)).
            Join(_anchor.DORotate(rot, ClipingDuration)).
            Join(_view.DOLocalMoveX(0, ClipingDuration)).
            OnComplete(() => _canMove = true);
    }

    private void FixedUpdate()
    {
        if (!_isActive) return;

        _rigidbody.velocity = _speed * Time.fixedDeltaTime * Vector3.forward;
    }
}

public enum StatePosition
{
    Left, Middle, Right
}