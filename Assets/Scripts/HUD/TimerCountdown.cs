using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Assertions;
using UnityEngine.UI;

public class TimerCountdown : MonoBehaviour
{
    private static int secondsMax = 5 *60;
    public static int SecondsMax { 
        private set
        {
            secondsMax = value;
        }
        
        get
        {
            return secondsMax;
        }
    }

    private int secondsLeft;

    public event Action<int> OnSecondChange;
    public event EventHandler OnCountdownEnd;

    void Start()
    {
        secondsLeft = secondsMax;
        StartCoroutine(TimerTake());
        
    }

    private IEnumerator TimerTake()
    {
        while (secondsLeft > 0)
        { 
            yield return new WaitForSeconds(1);
            secondsLeft -= 1;
            OnSecondChange?.Invoke(secondsLeft);

            VisualPollution.Instance.UpdateVisualPollution(ProgressBar.Instance.GetSlideValue());
        }

        OnCountdownEnd?.Invoke(this, EventArgs.Empty);        
    }
}
