using System;
using UnityEngine;

public class FunctionTimer
{
    private Action action;
    private float inputTimer;
    private float timer;

    public FunctionTimer(Action action, float timer)
    {
        this.action = action;
        this.timer = timer;
        inputTimer = timer;
    }

    public void Update()
    {
        timer -= Time.deltaTime;
        if (timer <= 0)
        {
            action();
            timer = inputTimer;
        }
    }
    public static void StartAndUpdateTimer(ref FunctionTimer timer, float delay, Action action)
    {
        if (timer == null)
            timer = new FunctionTimer(action, delay);
        timer.Update();

    }
}
