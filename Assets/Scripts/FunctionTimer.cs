using System;
using System.Collections;
using UnityEngine;

public class FunctionTimer : MonoBehaviour
{
    private bool _isOn = false;
    public void StartAndUpdateTimer(float delay, Action action = null)
    {
        if (!_isOn)
        {
            _isOn = false;

            StartCoroutine(WaitFor(delay));

            if (action != null)
                action();

            _isOn = true;
        }

    }
    private IEnumerator WaitFor(float delayInSeconds)
    {
        yield return new WaitForSeconds(delayInSeconds);
    }

    /* public static void StartAndUpdateTimer(ref FunctionTimer timer, float delay, params Action[] actions)
     {
         for (; timer._iteration <= actions.Length; timer._iteration++)
         {
             if (timer == null)
                 timer = new FunctionTimer(actions[timer._iteration], delay);
             timer.Update();
         }

     }*/
}