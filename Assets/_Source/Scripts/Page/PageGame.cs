using DG.Tweening;

public class PageGame : PanelBase
{
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

        Game.Action.OnEnter += Enter;
        Game.Action.OnLose += Exit;
        Game.Action.OnRestart += Enter;
    }

    protected override void OnShowComplated()
    {
        base.OnShowComplated();
        //Game.Action.SendStart();
    }
}