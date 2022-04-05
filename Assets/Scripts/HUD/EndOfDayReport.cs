using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EndOfDayReport : MonoBehaviour
{
    public Text DistanceTraveled;
    public Text PlayerName;
    public Text MostPlayedMinigame;
    public Text Success;
    public Text Fail;
    public Text SliderValue;
    public Text TotalTasknumber;
    public Text DayCondition;
    public Text timeBonus;


    private PlayerReportData playerReportData;
    
    void Start()
    {
        //This is temporary. Multiplayer implementation will change it.
        playerReportData = FindObjectOfType<PlayerReportData>();
        string distance = (playerReportData.totalDistance - (Math.Abs(playerReportData.startPosition.x))).ToString("F2");
        DistanceTraveled.text += $"  {distance} meters";
        //Will be changed in the future. Just for display purposes
        PlayerName.text += "  Jim Morrison";
        int playNr;
        MostPlayedMinigame.text +=$"  {playerReportData.GetTheMostPlayedMiniGameName(out playNr)}: {playNr}";
        Success.text += $"  {playerReportData.GetTheSuccessfulMinigameNumber()}";
        Fail.text += $"  {playerReportData.GetTheFailedMinigameNumber()}";
        SliderValue.text += $"  {ProgressBar.Instance.GetSlideValue().ToString("F2")}%";
        TotalTasknumber.text += $"  {playerReportData.GetTotalTaskNumber()}";
        DayCondition.text += $" Day is {getWinLoseCondition()} ";
        int remainingTime = TimerCountdown.Instance.GetRemainingTime();
        timeBonus.text += $"  is {remainingTime} seconds remaining ";
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
        
    }

    private string getWinLoseCondition()
    {
        if (ProgressBar.Instance.GetSlideValue() >= 80)
        {
            return "successfully finished.";
        }
        else
        {
            return "failed";
        }
    }

}
