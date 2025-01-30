using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VisualHandler : MonoBehaviour
{
    [SerializeField] private Transform _player;
    [SerializeField] private Transform _leftDoor, _rightDoor;
    [SerializeField] private float _durationRotate;
    [SerializeField] private float _durationMove;
    [SerializeField] private float _durationOpenDoor;


    private Sequence _sequence;

    private void Start()
    {
        Game.Action.OnEnter += Action_OnEnter;
        Game.Action.OnExit += Action_OnExit;
    }

    private void Action_OnEnter()
    {
        _sequence?.Kill();

        _sequence = DOTween.Sequence();

        _sequence.
            Append(_player.DOLocalRotate(Vector3.zero, _durationRotate)).
            Join(_leftDoor.DOLocalMoveX(1.4f, _durationOpenDoor)).
            Join(_rightDoor.DOLocalMoveX(-1.35f, _durationOpenDoor)).
            Join(_player.DOLocalMoveZ(0, _durationMove).SetEase(Ease.Linear).SetDelay(_durationRotate).OnComplete(Game.Action.SendStart));
    }

    private void Action_OnExit()
    {
        _player.localPosition = new Vector3(0, -1, -5);
        _player.localRotation = Quaternion.Euler(0, -180, 0);
        _leftDoor.localPosition = Vector3.zero;
        _rightDoor.localPosition = Vector3.zero;
    }
}