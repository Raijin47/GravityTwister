using DG.Tweening;

public class PanelSlot : PanelBase
{
    private readonly float Delay = 2f;

    protected override void Hide()
    {
        Game.Action.SendExit();

        _sequence.SetDelay(Delay).
            Append(_canvas.DOFade(0, _delay).From(1)).

            OnComplete(Game.Locator.PageMenu.Enter);
    }

    protected override void Show()
    {
        _sequence.
            Append(_canvas.DOFade(1, _delay).From(0)).
            Join(_components[0].DOScaleX(1, _delay).SetEase(Ease.OutBack).From(0)).
            Join(_components[1].DOScaleX(1, _delay).SetEase(Ease.OutBack).From(0)).
            Join(_components[2].DOScaleX(1, _delay).SetEase(Ease.OutBack).From(0)).
            Join(_components[3].DOScaleX(1, _delay).SetEase(Ease.OutBack).From(0)).
            Join(_components[4].DOScaleX(1, _delay).SetEase(Ease.OutBack).From(0)).

            OnComplete(Exit);
    }
}