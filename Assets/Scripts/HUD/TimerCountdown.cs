using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Assertions;
using UnityEngine.UI;

public class TimerCountdown : MonoBehaviour
{
    private int secondsLeft = 5 * 60;

    public event Action<string> OnSecondChange;
    public event EventHandler OnCountdownEnd;

    void Start()
    {
        StartCoroutine(TimerTake());
        
    }

    private IEnumerator TimerTake()
    {
        while (secondsLeft > 0)
        { 
            yield return new WaitForSeconds(1);
            secondsLeft -= 1;
            OnSecondChange?.Invoke(CountdownString());
        }

        OnCountdownEnd?.Invoke(this, EventArgs.Empty);        
    }

    public string CountdownString()
    {
        TimeSpan time = TimeSpan.FromSeconds(secondsLeft);
        return time.ToString(@"mm\:ss");
    }

}
