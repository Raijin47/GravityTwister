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

        foreach (ShopElement equpment in _equipments)
            equpment.Init();

        _equipments[_current].Frame = _enableImage;
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
            "Equipped" : "Unequip";
    }

    private void Action()
    {
        if(!_equipments[_current].IsPurchased())
        {
            if (Game.Wallet.Spend(100))
                _equipments[_current].Buy();
        }
        else
        {

        }
        
        



        UpdateUI();
    }

    private void OnValidate()
    {
        for(int i = 0; i < _equipments.Length; i++)
            _equipments[i].ID = i;
    }
}