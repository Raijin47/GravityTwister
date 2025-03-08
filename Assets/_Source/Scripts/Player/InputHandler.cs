using System;
using UnityEngine;

public class InputHandler : MonoBehaviour
{
    public event Action<bool> OnLeft;
    public event Action<bool> OnJump;

    private Vector2 _tapPosition;
    private Vector2 _swipeDelta;

    private readonly float DeadZone = 50;
    private bool _isSwiping;

    private bool IsHorizontal { get; set; }
    private bool IsInverse { get; set; }

    private void Start()
    {
        IsHorizontal = true;
        IsInverse = false;

        Game.Locator.Gravity.OnChangeGravity += Gravity_OnChangeGravity;
    }


    private void Update()
    {
        if (Input.touchCount > 0)
        {
            if (Input.GetTouch(0).phase == TouchPhase.Began)
            {
                _isSwiping = true;
                _tapPosition = Input.GetTouch(0).position;
            }
            else if (Input.GetTouch(0).phase == TouchPhase.Canceled ||
                Input.GetTouch(0).phase == TouchPhase.Ended)
            {
                ResetSwipe();
            }
        }

        CheckSwipe();
    }

    private void CheckSwipe()
    {
        if (_isSwiping)
        {
            if (Input.touchCount > 0)
                _swipeDelta = Input.GetTouch(0).position - _tapPosition;
        }

        if (_swipeDelta.magnitude > DeadZone)
        {
            if (Mathf.Abs(_swipeDelta.x) > Mathf.Abs(_swipeDelta.y))
            {
                if (IsHorizontal)
                {
                    if (_swipeDelta.x > 0) OnLeft?.Invoke(IsInverse);
                    else OnLeft?.Invoke(!IsInverse);            
                }
                else
                {
                    if (_swipeDelta.x > 0) OnJump?.Invoke(!IsInverse);
                    else OnJump?.Invoke(IsInverse);
                }
            }
            else
            {
                if(IsHorizontal)
                {
                    if (_swipeDelta.y > 0) OnJump?.Invoke(!IsInverse);
                    else OnJump?.Invoke(IsInverse);
                }
                else
                {
                    if (_swipeDelta.y > 0) OnLeft?.Invoke(!IsInverse);
                    else OnLeft?.Invoke(IsInverse);
                }
            }

            ResetSwipe();
        }
    }

    private void ResetSwipe()
    {
        _isSwiping = false;

        _tapPosition = Vector2.zero;
        _swipeDelta = Vector2.zero;
    }

    private void Gravity_OnChangeGravity(bool isHorizontal, bool isInverse)
    {
        IsHorizontal = isHorizontal;
        IsInverse = isInverse;
    }
}