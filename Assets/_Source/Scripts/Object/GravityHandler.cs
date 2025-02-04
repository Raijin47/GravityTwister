using DG.Tweening;
using System;
using UnityEngine;

public class GravityHandler : MonoBehaviour
{
    public event Action<Vector3, Vector3> OnChangeCling;
    public event Action<bool, bool> OnChangeGravity;

    private Transform _camera;
    private Sequence _sequence;
    private readonly float _delay = 1f;

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

    private void Awake() => _camera = Camera.main.transform;

    private void Start() 
    {
        Game.Action.OnEnter += Action_OnEnter;
        Game.Action.OnExit += Action_OnExit;
    }

    private void Action_OnExit()
    {
        Kill();

        _sequence.
            Append(_camera.DOLocalMove(new Vector3(0, 0, -8), _delay)).
            Join(_camera.DOLocalRotate(new Vector3(5, 0, 0), _delay));
    }

    private void Action_OnEnter() 
    { 
        Physics.gravity = DownG;

        Kill();

        _sequence.
            Append(_camera.DOLocalMove(new Vector3(0, 2, -8), _delay)).
            Join(_camera.DOLocalRotate(Vector3.zero, _delay));
    } 

    public void ApplyLeft()
    {
        Physics.gravity = LeftG;
        OnChangeGravity?.Invoke(false, false);
        OnChangeCling?.Invoke(LeftP, LeftR);

        Kill();

        _sequence.
            Append(_camera.DOLocalMove(new Vector3(2, 0, -8), _delay)).
            Join(_camera.DOLocalRotate(new Vector3(0, -5, 0), _delay));
    }

    public void ApplyRight()
    {
        Physics.gravity = RightG;
        OnChangeGravity?.Invoke(false, true);
        OnChangeCling?.Invoke(RightP, RightR);

        Kill();

        _sequence.
            Append(_camera.DOLocalMove(new Vector3(-2, 0, -8), _delay)).
            Join(_camera.DOLocalRotate(new Vector3(0, 5, 0), _delay));
    }

    public void ApplyUp()
    {
        Physics.gravity = UpG;
        OnChangeGravity?.Invoke(true, true);
        OnChangeCling?.Invoke(UpP, UpR);

        Kill();

        _sequence.
            Append(_camera.DOLocalMove(new Vector3(0, -2, -8), _delay)).
            Join(_camera.DOLocalRotate(Vector3.zero, _delay));
    }

    public void ApplyDown()
    {
        Physics.gravity = DownG;
        OnChangeGravity?.Invoke(true, false);
        OnChangeCling?.Invoke(DownP, Vector3.zero);

        Kill();

        _sequence.
            Append(_camera.DOLocalMove(new Vector3(0, 2, -8), _delay)).
            Join(_camera.DOLocalRotate(Vector3.zero, _delay));
    }

    private void Kill()
    {
        _sequence?.Kill();

        _sequence = DOTween.Sequence();
    }

#if UNITY_EDITOR
    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.A))
            ApplyLeft();

        if (Input.GetKeyDown(KeyCode.W))
            ApplyUp();

        if (Input.GetKeyDown(KeyCode.S))
            ApplyDown();

        if (Input.GetKeyDown(KeyCode.D))
            ApplyRight();
    }
#endif
}