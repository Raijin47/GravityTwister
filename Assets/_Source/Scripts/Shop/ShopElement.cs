using UnityEngine;
using UnityEngine.UI;

public class ShopElement : MonoBehaviour
{
    [SerializeField] private GameObject[] _equipment;
    [SerializeField] private int[] _prices;
    [SerializeField] private Sprite[] _sprites;
    [SerializeField] private Image _image;
    [SerializeField] private Image _frame;
    [SerializeField] private int _id;
    private int _current;

    public Sprite Frame
    {
        set
        {
            _frame.sprite = value;
        }
    }
    public int ID { get => _id; set => _id = value; }
    public int Current
    {
        get => _current;
        set
        {
            _equipment[_current].SetActive(false);

            _current = value;
            if (_current == _equipment.Length) _current = 0;
            if (_current < 0) _current = _equipment.Length - 1;

            _image.sprite = _sprites[_current];
            _equipment[_current].SetActive(true);
        }
    }
    public int Price => _prices[Current];

    public void Init()
    {
        _current = Game.Data.Saves.CurrentEquip[_id];
        _equipment[_current].SetActive(true);
    }

    public bool IsEquipped()
    {
        return Game.Data.Saves.CurrentEquip[_id] == _current;
    }

    public bool IsPurchased()
    {
        return Game.Data.Saves.IsPurchased[_id, _current];
    }

    public void Equip()
    {
        Game.Data.Saves.CurrentEquip[_id] = _current;
        Game.Data.SaveProgress();
    }
    public void Buy()
    {
        Game.Data.Saves.IsPurchased[_id, _current] = true;
        Game.Data.SaveProgress();
    }
}