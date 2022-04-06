using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;

public class UIManager : MonoBehaviour
{
    public TextMeshProUGUI hoverText;
    public TextMeshProUGUI trashText;
    public TextMeshProUGUI countDownText;
    public GameObject endMissionText;
    public GameObject endOfTheDayReportPrefab;
    

    public Image trashFillImage;
    //For the countdown
    public Color CountdownBeginningColor;
    public Color CountdownMiddleColor;
    public Color CountdownEndColor;

    private static UIManager _instance = null;
    public static UIManager Instance { get { return _instance; } }
    private Canvas canvas;
    private void Awake()
    {
        if (_instance != null && _instance != this)
            Destroy(this.gameObject);
        else
            _instance = this;

        canvas = GetComponent<Canvas>();

    }

    private void Start()
    {
        TimerCountdown timerCountdown = GetComponent<TimerCountdown>();
        if(TryGetComponent<TimerCountdown>(out timerCountdown)){
            timerCountdown.OnCountdownEnd += TimerCountdown_OnCountdownEnd;
            timerCountdown.OnSecondChange += TimerCountdown_OnSecondChange;

            //Setting the start of the countdown
            countDownText.text = CountdownString(TimerCountdown.SecondsMax);
        }

    }

    public void SetHoverText(string text)
    {
        if (text == null)
        {
            hoverText.text = "";
            return;
        }
        hoverText.text = $"(E) {text}";
    }
    public void ChangeCanvasShown()
    {
        canvas.enabled = !canvas.enabled;
    }
    public void SetTrashText(int currentAmount, int limit)
    {
        trashText.text = $"{currentAmount} / {limit}";
        trashFillImage.fillAmount = ((float)currentAmount / (float)limit);
        Debug.Log(((float)currentAmount / (float)limit));
    }
    public void BagFullAnim()
    {
        Animator anim = trashText.gameObject.GetComponent<Animator>();
        anim.SetTrigger("BagFull");
    }
    private void TimerCountdown_OnCountdownEnd(object sender, EventArgs e)
    {
        //Time ended so the progress bar animations has to stop.
        ProgressBar.Instance.isGameOngoing = false;
        Instantiate(endOfTheDayReportPrefab);
    }

    private void TimerCountdown_OnSecondChange(int countDown)
    {
        if (ProgressBar.Instance.GetSlideValue() == ProgressBar.Instance.GetSliderMaxValue())
        {
            Instantiate(endOfTheDayReportPrefab);
        }
        countDownText.text = CountdownString(countDown);
        ChangeColor(countDown);
    }

    public string CountdownString(int secondsLeft)
    {
        TimeSpan time = TimeSpan.FromSeconds(secondsLeft);
        return time.ToString(@"mm\:ss");
    }

    private void ChangeColor(int seconds)
    {
        if (seconds < 60)
        {
            if (IsColorChangeNeeded(CountdownEndColor))
            {
                countDownText.color = CountdownEndColor;
                countDownText.fontSize = countDownText.fontSize++;
                countDownText.gameObject.GetComponentInChildren<RawImage>().color = CountdownEndColor;
            }
        }
        else if (seconds < 120)
        {
            if (IsColorChangeNeeded(CountdownMiddleColor))
            {
                countDownText.color = CountdownMiddleColor;
                countDownText.fontSize = countDownText.fontSize++;
                countDownText.gameObject.GetComponentInChildren<RawImage>().color = CountdownMiddleColor;
            }
        }
        else
        {
            if (IsColorChangeNeeded(CountdownBeginningColor))
            {
                countDownText.color = CountdownBeginningColor;
                countDownText.gameObject.GetComponentInChildren<RawImage>().color = CountdownBeginningColor;
            }
        }

        if (seconds <= 10)
        {
            countDownText.gameObject.GetComponent<CharacterWobble>().enabled = true;
        }
    }

    private bool IsColorChangeNeeded(Color newColor)
    {
        return countDownText.color != newColor;
    }

}
