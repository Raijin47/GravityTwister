using System;
using UnityEngine;

public class InputHandler : MonoBehaviour
{
    public event Action<bool> OnLeft;

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
        if (IsHorizontal)
        {
            if (Input.GetKeyDown(KeyCode.LeftArrow))
                OnLeft?.Invoke(!IsInverse);

            if (Input.GetKeyDown(KeyCode.RightArrow))
                OnLeft?.Invoke(IsInverse);
        }
        else
        {
            if (Input.GetKeyDown(KeyCode.UpArrow))
                OnLeft?.Invoke(!IsInverse);

            if (Input.GetKeyDown(KeyCode.DownArrow))
                OnLeft?.Invoke(IsInverse);
        }
    }

    private void Gravity_OnChangeGravity(bool isHorizontal, bool isInverse)
    {
        IsHorizontal = isHorizontal;
        IsInverse = isInverse;
    }
}