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
        /*DynamicTranslator.Instance.translateEndOfTheDayVariables();*/

        //This is temporary. Multiplayer implementation will change it.
        playerReportData = FindObjectOfType<PlayerReportData>();
        string distance = (playerReportData.totalDistance - (Math.Abs(playerReportData.startPosition.x))).ToString("F2");
        DistanceTraveled.text += $"  {distance} m";

        int playNr;
        
        Success.text += $"  {playerReportData.GetTheSuccessfulMinigameNumber()}";
        Fail.text += $"  {playerReportData.GetTheFailedMinigameNumber()}";
        SliderValue.text += $"  {ProgressBar.Instance.GetSlideValue().ToString("F2")}%";
        TotalTasknumber.text += $"  {playerReportData.GetTotalTaskNumber()}";
       
        int remainingTime = TimerCountdown.Instance.GetRemainingTime();
 

            MostPlayedMinigame.text += $"  {playerReportData.GetTheMostPlayedMiniGameName(out playNr)} : {playNr} times";
            timeBonus.text += $"  {remainingTime} seconds remaining ";
            DayCondition.text += $"{getWinLoseCondition()} ";
        /*
            DayCondition.text = $"{getWinLoseConditionInDutch().ToString()}";
            Debug.Log(getWinLoseConditionInDutch());
            MostPlayedMinigame.text = $"{returnPrefabTaskNameInDutch(playerReportData.GetTheMostPlayedMiniGameName(out playNr)).ToString()} : {playNr} keer.";
            timeBonus.text += $"{remainingTime} seconden resterend";
        */


        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
        
    }


    private string returnPrefabTaskNameInDutch(string prefabname)
    {
        string value;
        switch (prefabname)
        {
            case "Clean sewers":
                value = "Schone riolen";
                break;

            case "Rewiring Street lamp":
                value = "Straatlantaarn opnieuw bedraden";
                break;
            case "Setting up solar panel":
                value = "Zonnepaneel opzetten";
                break;
            case "Plant trees":
                value = "Bomen planten";
                break;
            case "Converting street lamp to solar lamp":
                value = "Straatlantaarn ombouwen naar zonnelamp";
                break;
            default:
                value = "niet vragen";
                break;
        }
        return value;
    }

    private string getWinLoseConditionInDutch()
    {
        if (ProgressBar.Instance.GetSlideValue() >= 80)
        {
            return " Dag is succesvol afgesloten.";
        }
        else
        {
            return "Dag is mislukt";
        }
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
