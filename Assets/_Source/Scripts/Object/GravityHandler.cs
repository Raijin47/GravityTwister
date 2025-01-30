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

    public void ApplyLeft()
    {
        OnChangeGravity?.Invoke(false, false);
        OnChangeCling?.Invoke(LeftP, LeftR);
    }

    public void ApplyRight()
    {
        OnChangeGravity?.Invoke(false, true);
        OnChangeCling?.Invoke(RightP, RightR);
    }

    public void ApplyUp()
    {
        OnChangeGravity?.Invoke(true, true);
        OnChangeCling?.Invoke(UpP, UpR);
    }

    public void ApplyDown()
    {
        OnChangeGravity?.Invoke(true, false);
        OnChangeCling?.Invoke(DownP, Vector3.zero);
    }
}