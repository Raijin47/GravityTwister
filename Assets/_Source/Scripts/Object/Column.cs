using DG.Tweening;
using UnityEngine;

public class Column : MonoBehaviour
{
    private Tween _tween;
    private const float _delay = 3f;

    private void OnEnable()
    {
        bool rot = Random.value > 0.5f;
        bool pos = Random.value > 0.5f;

        transform.parent.rotation = Quaternion.Euler(0, 0, rot ? 0 : 90);
        transform.localPosition = new Vector3(pos ? .8f : -.8f, 0, 0);

        _tween?.Kill();

        _tween = transform.DOLocalMoveX(pos? -.8f : .8f, _delay).SetLoops(-1, LoopType.Yoyo).SetEase(Ease.Linear);
    }
}