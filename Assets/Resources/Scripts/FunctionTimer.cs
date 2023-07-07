using System;
using UnityEngine;

public class FunctionTimer
{
    private Action _action;
    private float _inputTimer;
    private float _timer;
    private bool _once;

    public FunctionTimer(Action action, float timer)
    {
        _action = action;
        _timer = timer;
        _inputTimer = timer;;
    }

    public void Update()
    {
        _timer -= Time.deltaTime;
        if (_timer <= 0)
        {
            if (_action != null) 
                _action.Invoke();
            if (!_once)
                _timer = _inputTimer;
        }
    }
    public static void StartAndUpdateTimer(ref FunctionTimer timer, float delay, Action action)
    {
        if (timer == null)
            timer = new FunctionTimer(action, delay);
        timer.Update();

    }
}
