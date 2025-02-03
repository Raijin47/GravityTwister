using System;
using UnityEngine;
using UnityEngine.UI;

public class Shop : MonoBehaviour
{
    [SerializeField] private EquipmentShop[] _equipments;
    [SerializeField] private ButtonBase _buttonNext, _buttonPreview;
    [SerializeField] private Sprite _disableImage, _enableImage;
    private int _current;

    private void Start()
    {
        _buttonNext.OnClick.AddListener(Next);
        _buttonPreview.OnClick.AddListener(Preview);

        foreach (EquipmentShop equpment in _equipments)
            equpment.Init();

        UpdateUI();
    }

    private void Next()
    {
        _equipments[_current].Current++;

        UpdateUI();
    }

    private void Preview()
    {
        _equipments[_current].Current--;

        UpdateUI();
    }

    public void SelectedEquipment(int value)
    {
        _equipments[_current].Frame = _disableImage;
        _current = value;
        _equipments[_current].Frame = _enableImage;

        UpdateUI();
    }

    private void UpdateUI()
    {

    }
}

[Serializable]
public class EquipmentShop
{
    [SerializeField] private GameObject[] _equipment;
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

    public void Init()
    {
        _current = Game.Data.Saves.CurrentEquip[_id];
        _equipment[_current].SetActive(true);
    }
}