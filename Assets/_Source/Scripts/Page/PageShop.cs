using DG.Tweening;
using UnityEngine;

public class PageShop : PanelBase
{
    [SerializeField] private ButtonBase _buttonBack;

    protected override void Hide()
    {
        _sequence.Append(_canvas.DOFade(0, _delay));
    }

    protected override void Show()
    {
        _sequence.SetDelay(_delay).
            Append(_canvas.DOFade(1, _delay)).


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
}
