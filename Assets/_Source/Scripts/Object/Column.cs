using DG.Tweening;
using UnityEngine;

public class Column : MonoBehaviour
{
    private Collider _collider;
    private Tween _tween;
    private const float _delay = 6f;

    private void Awake()
    {
        _collider = GetComponent<Collider>();
    }

    private void OnEnable()
    {
        bool rot = Random.value > 0.5f;
        bool pos = Random.value > 0.5f;

        transform.parent.rotation = Quaternion.Euler(0, 0, rot ? 0 : 90);
        transform.localPosition = new Vector3(pos ? 1.5f : -1.5f, 0, 0);

        _tween?.Kill();
        _collider.enabled = true;

        _tween = transform.DOLocalMoveX(pos? -1.5f : 1.5f, _delay).SetLoops(-1, LoopType.Yoyo).SetEase(Ease.Linear);
    }

    private void OnTriggerEnter(Collider other) 
    {
        _collider.enabled = false;
        Game.Action.SendLose();
    }
}