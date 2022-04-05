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
    private int startCountDownLeft = 5;

    public event Action<int> OnSecondChange;
    public event Action<string> OnStartCountdownChange;
    public event EventHandler OnCountdownEnd;

    void Start()
    {
        secondsLeft = secondsMax;
        MiniGameManager.Instance.FreezeScreen(true);
        StartCoroutine(StartCountDown());
        
    }

    private IEnumerator StartCountDown()
    {
        while (startCountDownLeft > 0)
        {
            yield return new WaitForSeconds(1);
            startCountDownLeft -= 1;

            if (startCountDownLeft <= 0)
            {
                OnStartCountdownChange?.Invoke("Go!");
                StartCoroutine(TimerTake());
                yield break;
            }
            else if (startCountDownLeft <= 3)
            {
                OnStartCountdownChange?.Invoke(startCountDownLeft.ToString());
            }
        }
    }

    private IEnumerator TimerTake()
    {
        MiniGameManager.Instance.FreezeScreen(false);

        while (secondsLeft > 0)
        { 
            yield return new WaitForSeconds(1);
            secondsLeft -= 1;
            OnSecondChange?.Invoke(secondsLeft);
        }

        OnCountdownEnd?.Invoke(this, EventArgs.Empty);        
    }
}
