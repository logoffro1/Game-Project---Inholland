using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Assertions;
using UnityEngine.UI;

public class TimerCountdown : MonoBehaviour
{
    public TextMeshProUGUI countdownTextDisplay;
    public GameObject endMissionText;
    public int secondsLeft = 5 * 60;
    private bool takingAway = false;

    void Start()
    {
        if (countdownTextDisplay != null) countdownTextDisplay.text = CountdownString();
    }

    void Update()
    {
        if (!takingAway && secondsLeft > 0)
        {
            StartCoroutine(TimerTake());
        }

        if (secondsLeft <= 0)
        {
            endMissionText.SetActive(true);
            //TODO: wait few seconds
            //TODO: switch to end of report scene
        }
    }

    private IEnumerator TimerTake()
    {
        if (countdownTextDisplay != null)
        {
            takingAway = true;
            yield return new WaitForSeconds(1);
            secondsLeft -= 1;
            countdownTextDisplay.text = CountdownString();
            takingAway = false;
        }
    }

    private string CountdownString()
    {
        TimeSpan time = TimeSpan.FromSeconds(secondsLeft);
        return time.ToString(@"mm\:ss");
    }

}
