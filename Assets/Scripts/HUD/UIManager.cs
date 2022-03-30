using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;

public class UIManager : MonoBehaviour
{
    public TextMeshProUGUI hoverText;
    public TextMeshProUGUI countDownText;
    public GameObject endMissionText;

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
        timerCountdown.OnCountdownEnd += TimerCountdown_OnCountdownEnd;
        timerCountdown.OnSecondChange += TimerCountdown_OnSecondChange;

        //Setting the start of the countdown
        countDownText.text = timerCountdown.CountdownString();
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

    private void TimerCountdown_OnCountdownEnd(object sender, EventArgs e)
    {
        endMissionText.SetActive(true);
        //TODO: wait few seconds
        //TODO: switch to end of report scene
    }

    private void TimerCountdown_OnSecondChange(string countDown)
    {
        countDownText.text = countDown;
    }

}
