using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Localization;
using UnityEngine.Localization.Settings;
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
        DynamicTranslator.Instance.translateEndOfTheDayVariables();

        //This is temporary. Multiplayer implementation will change it.
        playerReportData = FindObjectOfType<PlayerReportData>();
        string distance = (playerReportData.totalDistance - (Math.Abs(playerReportData.startPosition.x))).ToString("F2");
        DistanceTraveled.text += $"  {distance} m";

        int playNr;
        MostPlayedMinigame.text +=$"  {playerReportData.GetTheMostPlayedMiniGameName(out playNr)} : {playNr} times";
        Success.text += $"  {playerReportData.GetTheSuccessfulMinigameNumber()}";
        Fail.text += $"  {playerReportData.GetTheFailedMinigameNumber()}";
        SliderValue.text += $"  {ProgressBar.Instance.GetSlideValue().ToString("F2")}%";
        TotalTasknumber.text += $"  {playerReportData.GetTotalTaskNumber()}";
        DayCondition.text += $"{getWinLoseCondition()} ";
        int remainingTime = TimerCountdown.Instance.GetRemainingTime();
        timeBonus.text += $"  {remainingTime} seconds remaining ";


        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
        
    }

   
    private string getWinLoseCondition()
    {
        if (ProgressBar.Instance.GetSlideValue() >= 80)
        {
            return " Day is successfully finished.";
        }
        else
        {
            return "Day is failed";
        }
    }

   


}
