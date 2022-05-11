using System;
using System.Collections;
using System.Linq;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Assertions;

public class TimerCountdown : MonoBehaviour
{
    private int secondsMax = 8*60;

    private static TimerCountdown _instance;
    public static TimerCountdown Instance { get { return _instance; } }
    public int SecondsMax { 
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
    public int SecondsLeft
    {
        get { return secondsLeft; }
        set { secondsLeft = value; }
    }

    public int StartCountDownLeft
    {
        get { return startCountDownLeft; }
        private set { startCountDownLeft = value; }
    }

    private int startCountDownLeft = 4;

    public event Action<int> OnSecondChange;
    public event Action<string> OnStartCountdownChange;
    public event EventHandler OnCountdownEnd;
    private GameMode gameMode;

    void Start()
    {
        secondsLeft = secondsMax;
        MiniGameManager.Instance.FreezeScreen(true);
        gameMode = FindObjectOfType<PlayerData>().IsInGameMode;
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

                yield return new WaitForSeconds(1);

                MiniGameManager.Instance.FreezeScreen(false);
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
        while (secondsLeft > 0)
        {
            if (ProgressBar.Instance.GetSlideValue() == ProgressBar.Instance.GetSliderMaxValue())
            {
                break;
            }
            yield return new WaitForSeconds(1);
            if (gameMode != GameMode.Chill) secondsLeft -= 1;
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
