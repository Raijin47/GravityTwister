using DG.Tweening;
using TMPro;
using UnityEngine;

public class PageLose : PanelBase
{
    [SerializeField] private TextMeshProUGUI _text;
    private int _earningCoin;

    protected override void Hide()
    {
        _sequence.Append(_canvas.DOFade(0, _delay)).
            Join(_components[0].DOScale(2, _delay)).
            Join(_components[1].DOLocalMoveX(-300, _delay).SetEase(Ease.InBack)).
            Join(_components[2].DOScale(0, _delay).SetEase(Ease.InBack)).
            Join(_components[3].DOLocalMoveX(300, _delay).SetEase(Ease.InBack));
    }

    protected override void Show()
    {
        float distance = Game.Locator.Player.transform.position.z;
        _earningCoin = (int)(Game.Locator.Watch.CurrentTime / 60 * distance);

        _text.text = $"{Game.Locator.Watch.GetTime()}\n" +
            $"{Mathf.Round(Game.Locator.Player.transform.position.z)}m\n" +
            $"{_earningCoin}<sprite=0>";

        _sequence.SetDelay(_delay).
            Append(_canvas.DOFade(1, _delay)).

            Join(_components[0].DOScale(1, _delay).From(2)).
            Join(_components[1].DOLocalMoveX(0, _delay).SetEase(Ease.OutBack).From(-300)).
            Join(_components[2].DOScale(1, _delay).SetEase(Ease.OutBack).From(0)).
            Join(_components[3].DOLocalMoveX(0, _delay).SetEase(Ease.OutBack).From(300)).
            
            OnComplete(OnShowComplated);
    }

    protected override void Start()
    {
        base.Start();

        Game.Action.OnLose += Enter;
        Game.Action.OnExit += Exit;
        Game.Action.OnRestart += Exit;
    }

    protected override void OnShowComplated()
    {
        base.OnShowComplated();
        Game.Wallet.Add(_earningCoin);
    }
}