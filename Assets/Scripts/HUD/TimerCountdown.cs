using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Assertions;
using UnityEngine.UI;

public class TimerCountdown : MonoBehaviour
{
    private static int secondsMax = 5*60;

    private static TimerCountdown _instance;
    public static TimerCountdown Instance { get { return _instance; } }
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
    private void Awake()
    {
        if (_instance != null && _instance != this)
        {
            Destroy(this.gameObject);
        }
        else
        {
            _instance = this;
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
            if (ProgressBar.Instance.GetSlideValue() == ProgressBar.Instance.GetSliderMaxValue())
            {
                break;
            }
            yield return new WaitForSeconds(1);
            secondsLeft -= 1;
            OnSecondChange?.Invoke(secondsLeft);

            VisualPollution.Instance.UpdateVisualPollution(ProgressBar.Instance.GetSlideValue());
        }

        OnCountdownEnd?.Invoke(this, EventArgs.Empty);        
    }
    public int GetRemainingTime()
    {
        return secondsLeft;
    }
}
