using System;
using UnityEngine;

[Serializable]
public class Locator
{
    [SerializeField] private PlayerBase _player;
    [SerializeField] private InputHandler _input;
    [SerializeField] private GravityHandler _gravity;

    public PlayerBase Player => _player;
    public GravityHandler Gravity => _gravity;
    public InputHandler Input => _input;
}