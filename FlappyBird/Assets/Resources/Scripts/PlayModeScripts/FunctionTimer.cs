using System;
using UnityEngine;

public class FunctionTimer
{
    private Action _action;
    private float _inputTimer,
                  _timer;
    private bool _once;

    public FunctionTimer(Action action, float timer)
    {
        _action = action;
        _timer = timer;
        _inputTimer = timer;
    }

    public void Update()
    {
        _timer -= Time.deltaTime;
        if (_timer <= 0)
        {
            _action?.Invoke();
            if (!_once)
                _timer = _inputTimer;
        }
    }

    public static void StartAndUpdateTimer(ref FunctionTimer timer, float delay, Action action)
    {
        timer ??= new FunctionTimer(action, delay);
        timer.Update();
    }
}
