using System.Collections;
using UnityEngine;
using TMPro;

public class Watch : MonoBehaviour
{
    private TextMeshProUGUI _text;
    private Coroutine _coroutine;

    private float _currentTime;

    public float CurrentTime 
    {
        get => _currentTime;
        set
        {
            _currentTime = value;
            _text.text = TextUtility.FormatMinute(_currentTime);
        }
    }
    public string GetTime()
    {
        string text = TextUtility.FormatMinute(_currentTime);
        return text;    
    }

    private void Start()
    {
        _text = GetComponent<TextMeshProUGUI>();
        Game.Action.OnPause += Action_OnPause;
        Game.Action.OnEnter += Action_OnEnter;
        Game.Action.OnStart += Action_OnStart;
        Game.Action.OnRestart += Action_OnEnter;
    }

    private void Action_OnEnter() => CurrentTime = 0;

    private void Action_OnStart()
    {
        Release();
        _coroutine = StartCoroutine(UpdateProcess());
    }

    private void Action_OnPause(bool onPause)
    {
        Release();
        if (!onPause) _coroutine = StartCoroutine(UpdateProcess()); 
    }

    private IEnumerator UpdateProcess()
    {
        while (true)
        {
            CurrentTime += Time.deltaTime;
            yield return null;
        }        
    }

    private void Release()
    {
        if(_coroutine != null)
        {
            StopCoroutine(_coroutine);
            _coroutine = null;
        }
    }
}