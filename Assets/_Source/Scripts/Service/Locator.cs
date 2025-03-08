using System;
using UnityEngine;

[Serializable]
public class Locator
{
    [SerializeField] private PlayerBase _player;
    [SerializeField] private InputHandler _input;
    [SerializeField] private GravityHandler _gravity;
    [SerializeField] private PageMenu _pageMenu;
    //[SerializeField] private PageShop _pageShop;
    //[SerializeField] private Shop _shop;
    [SerializeField] private ChunkSpawner _spawner;
    [SerializeField] private Watch _watch;
    [SerializeField] private SpinController _spinController;
    [SerializeField] private PanelSlot _panelSlot;
    [SerializeField] private PageLose _pageLose;

    public PlayerBase Player => _player;
    public GravityHandler Gravity => _gravity;
    public InputHandler Input => _input;
    public PageMenu PageMenu => _pageMenu;
    //public PageShop PageShop => _pageShop;
    //public Shop Shop => _shop;
    public ChunkSpawner Spawner => _spawner;
    public Watch Watch => _watch;
    public SpinController SpinController => _spinController;
    public PanelSlot PanelSlot => _panelSlot;
    public PageLose PageLose => _pageLose;
}