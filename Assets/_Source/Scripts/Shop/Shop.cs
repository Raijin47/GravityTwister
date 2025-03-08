using TMPro;
using UnityEngine;

public class Shop : MonoBehaviour
{
    [SerializeField] private ShopElement[] _equipments;
    [SerializeField] private TextMeshProUGUI _buttonText;
    [SerializeField] private ButtonBase _buttonNext, _buttonPreview;
    [SerializeField] private ButtonBase _buttonAction;
    [SerializeField] private Sprite _disableImage, _enableImage;

    private int _current;

    private void Start()
    {
        _buttonNext.OnClick.AddListener(Next);
        _buttonPreview.OnClick.AddListener(Preview);
        _buttonAction.OnClick.AddListener(Action);

        //Game.Locator.PageShop.OnClose += PageShop_OnClose;

        foreach (ShopElement equpment in _equipments)
            equpment.Init();

        _equipments[_current].Frame = _enableImage;
        UpdateUI();
    }

    private void PageShop_OnClose()
    {
        foreach (ShopElement equpment in _equipments)
            equpment.SetDefault();

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
        _buttonText.text = _equipments[_current].IsEquipped() ? 
            "ACTIVE" : _equipments[_current].IsPurchased() ?
            "EQUIP" : $"{_equipments[_current].Price}<sprite=0>";
    }

    private void Action()
    {
        if(!_equipments[_current].IsPurchased())
        {
            if (Game.Wallet.Spend(_equipments[_current].Price))
                _equipments[_current].Buy();
        }
        else
        {
            if (_equipments[_current].IsEquipped()) return;
            else _equipments[_current].Equip();
        }
             
        UpdateUI();
    }

    private void OnValidate()
    {
        for(int i = 0; i < _equipments.Length; i++)
            _equipments[i].ID = i;
    }
}