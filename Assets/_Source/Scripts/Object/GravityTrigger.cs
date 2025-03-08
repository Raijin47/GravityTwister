using DG.Tweening;
using UnityEngine;

public class GravityTrigger : MonoBehaviour
{
    [SerializeField] private Transform _eggTransform;
    [SerializeField] private ParticleSystem _particle;
    private Tween _tween;

    public GravityHook Gravity { get; set; }
   
    [SerializeField] private ElectroFloor _floor;

    private Collider _collider;

    private readonly Vector3[] Pos = new Vector3[3]
    {
        new Vector3(-1.5f, 0, 0),
        new Vector3(1.5f, 0, 0),
        Vector3.zero
    };

    private void Awake() => _collider = GetComponent<Collider>();

    private void PlayParticle() => _particle.Play();

    private void OnTriggerEnter(Collider other)
    {
        _collider.enabled = false;
        _floor.Disable();
        Game.Audio.PlayClip(0);

        _tween?.Kill();

        _tween = _eggTransform.DOScale(0, .2f)
            .SetEase(Ease.InBounce)
            .OnComplete(PlayParticle);

        int random = Random.Range(1, 20);

        Game.Locator.PageLose.EarningCoin += random;
        Game.Wallet.Add(random);
      
        int r = Random.Range(0, 2);
        var gravity = Game.Locator.Gravity;

        switch (Gravity)
        {
            case GravityHook.Left:
                if (r == 0) gravity.ApplyUp();
                if (r == 1) gravity.ApplyDown();
                if (r == 2) gravity.ApplyRight();
                break;

            case GravityHook.Right:
                if (r == 0) gravity.ApplyUp();
                if (r == 1) gravity.ApplyDown();
                if (r == 2) gravity.ApplyLeft();
                break;

            case GravityHook.Up:
                if (r == 0) gravity.ApplyLeft();
                if (r == 1) gravity.ApplyDown();
                if (r == 2) gravity.ApplyRight();
                break;

            case GravityHook.Down:
                if (r == 0) gravity.ApplyUp();
                if (r == 1) gravity.ApplyLeft();
                if (r == 2) gravity.ApplyRight();
                break;
        }
    }

    private void OnEnable()
    {
        transform.localPosition = Pos[Random.Range(0, Pos.Length)];

        _eggTransform.localScale = Vector3.one;

        if (_collider != null)
            _collider.enabled = true;
    }
}

public enum GravityHook
{
    Left, Right, Up, Down
}