using DG.Tweening;
using System;
using UnityEngine;

public class PageShop : PanelBase
{
    public event Action OnClose;
    [SerializeField] private ButtonBase _buttonBack;

    protected override void Hide()
    {
        _sequence.Append(_canvas.DOFade(0, _delay)).

            Join(_components[0].DOLocalMoveX(-300, _delay).SetEase(Ease.InBack)).
            Join(_components[1].DOLocalMoveX(300, _delay).SetEase(Ease.InBack)).
            Join(_components[2].DOScale(0, _delay).SetEase(Ease.InBack)).
            Join(_components[3].DOScale(0, _delay).SetEase(Ease.InBack)).

        OnComplete(Close);
    }

    protected override void Show()
    {
        _sequence.SetDelay(_delay).
            Append(_canvas.DOFade(1, _delay)).

            Join(_components[0].DOLocalMoveX(0, _delay).SetEase(Ease.OutBack).From(-300)).
            Join(_components[1].DOLocalMoveX(0, _delay).SetEase(Ease.OutBack).From(300)).
            Join(_components[2].DOScale(1, _delay).SetEase(Ease.OutBack).From(0)).
            Join(_components[3].DOScale(1, _delay).SetEase(Ease.OutBack).From(0)).

        OnComplete(OnShowComplated);
    }

    protected override void Start()
    {
        base.Start();

        _buttonBack.OnClick.AddListener(BackToMenu);
    }

    private void BackToMenu()
    {
        Exit();
        Game.Locator.PageMenu.Enter();
    }

    private void Close() => OnClose?.Invoke();
}
