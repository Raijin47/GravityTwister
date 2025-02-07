using UnityEngine;

public class SpinController : MonoBehaviour
{
    [SerializeField] private Slot[] _slots;

    [SerializeField] private AudioClip _clip;

    [Space(10)]
    [SerializeField] private float _offsetVertical;
    [SerializeField] private float _offsetHorizontal;

    private const float _startSpeed = 800;
    public float Offset => _offsetVertical;

    private void OnEnable()
    {
        foreach (Slot slot in _slots) slot.SetPosition(_offsetVertical);
    }

    public void Spin()
    {
        Game.Audio.PlayClip(_clip);

        float speed = _startSpeed * (Random.value + 1);
        float increaseSpeed = Random.value + 1;

        for (int i = 0; i < _slots.Length; i++)
        {
            _slots[i].Spin(speed);
            speed *= increaseSpeed;
        }
    }

    public void GetResult()
    {
        bool isActive = true;

        foreach (Slot slot in _slots)
            if (slot.IsActive) isActive = false;

        if (!isActive) return;

        int countWin = 0;

        for (int i = 0; i < _slots.Length; i++)
            for (int a = i + 1; a < _slots.Length; a++)
                if (_slots[i].Result == _slots[a].Result)          
                    countWin++;

                

        if (countWin >= 1) Game.Wallet.Add(Game.Locator.PageLose.EarningCoin * countWin);

        Game.Locator.PanelSlot.Exit();
        Game.Locator.PageMenu.Enter();

    }

    private void OnValidate()
    {
        _slots = gameObject.GetComponentsInChildren<Slot>();

        float totalHeight = _slots.Length * _offsetHorizontal;

        for (int i = 0; i < _slots.Length; i++)
        {
            float pos = -totalHeight / 2 + i * _offsetHorizontal + _offsetHorizontal / 2;
            if (Mathf.Abs(pos) < 0.1f) pos = 0;

            RectTransform transform = _slots[i].GetComponent<RectTransform>();
            transform.anchoredPosition = new Vector2(pos, 0);
        }

        foreach (Slot slot in _slots) slot.SetPosition(_offsetVertical);
    }
}