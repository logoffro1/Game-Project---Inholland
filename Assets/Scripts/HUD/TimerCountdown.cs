using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class TimerCountdown : MonoBehaviour
{
    public TextMeshProUGUI textDisplay;
    public GameObject endMissionText;
    public int secondsLeft = 30;
    private bool takingAway = false;

    void Start()
    {
        textDisplay.text = "00:" + secondsLeft;
    }

    void Update()
    {
        if (!takingAway && secondsLeft > 0)
        {
            StartCoroutine(TimerTake());
        }

        //tHIS IS NOT WORKING
        if (secondsLeft < 0)
        {
            endMissionText.SetActive(true);
            //wait few seconds
            //switch to end of report scene
        }
    }

    IEnumerator TimerTake()
    {
        takingAway = true;
        yield return new WaitForSeconds(1);
        secondsLeft -= 1;
        if (secondsLeft < 10)
        {
            textDisplay.text = "00:0" + secondsLeft;
        }
        else
        {
            textDisplay.text = "00:" + secondsLeft;
        }
        takingAway = false;
    }

}
