using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Shop : MonoBehaviour
{
    [SerializeField] protected GameObject[] _equipments;
    [SerializeField] private ButtonBase _buttonNext, _buttonPreview;

    protected int _current;

    private void Start()
    {
        Init();

        _buttonNext.OnClick.AddListener(Next);
        _buttonPreview.OnClick.AddListener(Preview);
    }

    private void Next()
    {
        _current++;

        if(_current == _equipments.Length)
            _current = 0;

        UpdateUI();
    }

    private void Preview()
    {
        _current--;

        if (_current < 0)
            _current = _equipments.Length - 1;

        UpdateUI();
    }

    protected abstract void Init();

    private void UpdateUI()
    {

    }
}