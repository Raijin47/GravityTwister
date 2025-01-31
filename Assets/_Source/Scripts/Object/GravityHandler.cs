using System;
using UnityEngine;

public class GravityHandler : MonoBehaviour
{
    public event Action<Vector3, Vector3> OnChangeCling;
    public event Action<bool, bool> OnChangeGravity;

    private readonly Vector3 LeftP = new(-1.5f, 0, 0);
    private readonly Vector3 RightP = new(1.5f, 0, 0);
    private readonly Vector3 UpP = new(0, 1.5f, 0);
    private readonly Vector3 DownP = new(0, -1.5f, 0);

    private readonly Vector3 LeftR = new(0, 0, -90);
    private readonly Vector3 RightR = new(0, 0, 90);
    private readonly Vector3 UpR = new(0, 0, 180);

    private readonly Vector3 LeftG = new(-9.81f, 0, 0);
    private readonly Vector3 RightG = new(9.81f, 0, 0);
    private readonly Vector3 UpG = new(0, 9.81f, 0);
    private readonly Vector3 DownG = new (0, -9.81f, 0);

    private void Start() => Game.Action.OnEnter += Action_OnEnter;
    private void Action_OnEnter() => Physics.gravity = DownG;

    public void ApplyLeft()
    {
        Physics.gravity = LeftG;
        OnChangeGravity?.Invoke(false, false);
        OnChangeCling?.Invoke(LeftP, LeftR);
    }

    public void ApplyRight()
    {
        Physics.gravity = RightG;
        OnChangeGravity?.Invoke(false, true);
        OnChangeCling?.Invoke(RightP, RightR);
    }

    public void ApplyUp()
    {
        Physics.gravity = UpG;
        OnChangeGravity?.Invoke(true, true);
        OnChangeCling?.Invoke(UpP, UpR);
    }

    public void ApplyDown()
    {
        Physics.gravity = DownG;
        OnChangeGravity?.Invoke(true, false);
        OnChangeCling?.Invoke(DownP, Vector3.zero);
    }
}