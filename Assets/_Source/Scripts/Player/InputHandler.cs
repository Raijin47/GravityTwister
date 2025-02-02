using System;
using UnityEngine;

public class InputHandler : MonoBehaviour
{
    public event Action<bool> OnLeft;
    public event Action OnJump;

    private Vector2 _startTouchPosition;
    private Vector2 _endTouchPosition;

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
        //if (IsHorizontal)
        //{
        //    if (Input.GetKeyDown(KeyCode.LeftArrow))
        //        OnLeft?.Invoke(!IsInverse);

        //    if (Input.GetKeyDown(KeyCode.RightArrow))
        //        OnLeft?.Invoke(IsInverse);
        //}
        //else
        //{
        //    if (Input.GetKeyDown(KeyCode.UpArrow))
        //        OnLeft?.Invoke(!IsInverse);

        //    if (Input.GetKeyDown(KeyCode.DownArrow))
        //        OnLeft?.Invoke(IsInverse);
        //}

        if (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Began)
            _startTouchPosition = Input.GetTouch(0).position;

        if (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Ended)
        {
            _endTouchPosition = Input.GetTouch(0).position;

            if (IsHorizontal)
            {
                OnJump?.Invoke();
                //if (_endTouchPosition.x > _startTouchPosition.x)
                //    OnLeft?.Invoke(IsInverse);

                //if (_endTouchPosition.x < _startTouchPosition.x)
                //    OnLeft?.Invoke(!IsInverse);
            }
            else
            {
                if (_endTouchPosition.y < _startTouchPosition.y)
                    OnLeft?.Invoke(IsInverse);

                if (_endTouchPosition.y > _startTouchPosition.y)
                    OnLeft?.Invoke(!IsInverse);
            }
        }
    }

    private void Gravity_OnChangeGravity(bool isHorizontal, bool isInverse)
    {
        IsHorizontal = isHorizontal;
        IsInverse = isInverse;
    }
}