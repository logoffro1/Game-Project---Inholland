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
    private PlayFabManager playFabManager;




    private PlayerReportData playerReportData;
    
    void Start()
    {
       

        playFabManager = FindObjectOfType<PlayFabManager>();
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
        writePlayFabData();
      
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
        
    }

    private void writePlayFabData()
    {
        playFabManager.SendLeaderBoard(playerReportData.GetTheSuccessfulMinigameNumber());
        playFabManager.WriteCustomPlayerEvent("Distance_travelled_per_game",new Dictionary<string, object> {
            { "DistanceTravelled" ,(playerReportData.totalDistance - (Math.Abs(playerReportData.startPosition.x))).ToString("F2") }

        });
        playFabManager.WriteCustomPlayerEvent("Number_of_successful_tasks_per_game", new Dictionary<string, object> {
            { "SuccessfulTasks" ,playerReportData.GetTheSuccessfulMinigameNumber() }
        });
        playFabManager.WriteCustomPlayerEvent("Number_of_failed_tasks_per_game", new Dictionary<string, object> {
            { "FailedTasks" ,playerReportData.GetTheFailedMinigameNumber() }
        });
        playFabManager.WriteCustomPlayerEvent("Sustainibility_level_per_game", new Dictionary<string, object> {
            { "SustainibilityLevel" ,ProgressBar.Instance.GetSlideValue().ToString("F2") }
        });
        playFabManager.WriteCustomPlayerEvent("Total_number_of minigames_played_per_game", new Dictionary<string, object> {
            { "TotalNrTasks" ,playerReportData.GetTotalTaskNumber() }
        });
        playFabManager.WriteCustomPlayerEvent("Remaining_time_after_the_game", new Dictionary<string, object> {
            { "RemainingTime" ,TimerCountdown.Instance.GetRemainingTime().ToString() }
        });

        /*  playFabManager.SendEndOfTheDayReportData(new Dictionary<string, string>
          {
              { "DistanceTravelled" , DistanceTraveled.text},
              { "PlayerName", "John"},
              { "SuccessfulMissions", Success.text},
              { "FailedMissions",Fail.text},
              { "SustainibilityLevel",SliderValue.text},
              { "TotalNumberOfMinigames",TotalTasknumber.text},
              { "Time Remaining",TimerCountdown.Instance.GetRemainingTime().ToString()},
          }
          );*/
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
