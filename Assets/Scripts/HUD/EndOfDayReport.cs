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
    public Text Location;
    public Text PlayerName;
    public Text MostPlayedMinigame;
    public Text Success;
    public Text Fail;
    public Text SliderValue;
    public Text TotalTasknumber;
    public Text DayCondition;
    public Text timeBonus;
    public Text Income;
    private PlayFabManager playFabManager;
    public AudioClip DayReportAudio;

    private PlayerReportData playerReportData;
    private PlayerReputation reputation;
    private bool dayFailed = false;
    private string lang = "en";

    private PlayerData playerData;

    void Start()
    {
        GetLanguage();
        dayFailed = GetWinCondition();

        playerData = FindObjectOfType<PlayerData>();
        playerData.NewSustainabilityPoints = dayFailed ? -0.05f : (ProgressBar.Instance.GetSlideValue() / 2000); //Change 
        playerData.AddToCurrentDistrict(playerData.NewSustainabilityPoints);
        DontDestroyOnLoad(playerData.gameObject);

        playFabManager = FindObjectOfType<PlayFabManager>();
        /*DynamicTranslator.Instance.translateEndOfTheDayVariables();*/

        //This is temporary. Multiplayer implementation will change it.

        playerReportData = FindObjectOfType<PlayerReportData>();

        reputation = GameObject.FindObjectOfType<PlayerReputation>();

        //MERGE POINT SYSTEM INTO DEV A S A P
      /*
           reputation.IncreaseEXP(TimerCountdown.Instance.SecondsLeft,
            playerReportData.GetHardGameNumbers(),
            playerReportData.GetMediumGameNumbers(),
            playerReportData.GetEasyGameNumbers(),
            dayFailed);
*/
        string distance = (playerReportData.totalDistance - (Math.Abs(playerReportData.startPosition.x))).ToString("F2");
        DistanceTraveled.text += $"{distance} m";

        int playNr;

        Location.text = playerData.IsInDistrict.ToString();
        Success.text += $"{playerReportData.GetTheSuccessfulMinigameNumber()}";
        Fail.text += $"{playerReportData.GetTheFailedMinigameNumber()}";
        SliderValue.text += $"{ProgressBar.Instance.GetSlideValue().ToString("F2")}%";
        TotalTasknumber.text += $"{playerReportData.GetTotalTaskNumber()}";
        Income.text = $"{GetIncome()} SP"; //SP == Sustainability Points -> change

        writePlayFabData();
        MostPlayedMinigame.text += $"{playerReportData.GetTheMostPlayedMiniGameName(out playNr)} : {playNr} times";
        timeBonus.text += GetSecondsRemainingText();
        DayCondition.text += $"{GetWinLoseText()} ";

        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
        Time.timeScale = 0f;

        GetComponent<AudioSource>().PlayOneShot(DayReportAudio);
    }
    private string GetSecondsRemainingText()
    {

        int remainingTime = TimerCountdown.Instance.GetRemainingTime();
        switch (lang)
        {
            case "en":
                return $"{remainingTime} seconds";
            case "nl":
                return $"{remainingTime} seconden";
            case "ro":
                return $"{remainingTime} secunde";
            default:
                return $"{remainingTime} seconds ";


        }
    }
    private string GetWinLoseText()
    {

        if (!dayFailed)
        {
            switch (lang)
            {
                case "en":
                    return "Success";
                case "nl":
                    return "Succes";
                case "ro":
                    return "Succes";
                default:
                    return "Success";
            }
        }
        else
            switch (lang)
            {
                case "en":
                    return "Fail";
                case "nl":
                    return "Mislukking";
                case "ro":
                    return "E?ueaz?";

                default:
                    return "Fail.";
            }
    }
    private bool GetWinCondition()
    {
        return !(ProgressBar.Instance.GetSlideValue() >= 80f);

    }
    private async void GetLanguage()
    {
        var handle = LocalizationSettings.InitializationOperation;
        await handle.Task;
        LocalizationSettings locSettings = handle.Result;

        lang = locSettings.GetSelectedLocale().Identifier.Code;
    }
    private void writePlayFabData()
    {
        playFabManager.SendLeaderBoard(playerReportData.GetTheSuccessfulMinigameNumber());
        playFabManager.WriteCustomPlayerEvent("Distance_travelled_per_game", new Dictionary<string, object> {
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
        playFabManager.WriteCustomPlayerEvent("Number_of_trash_pieces_disposed", new Dictionary<string, object> {
            { "nrOfTrashDisposed" ,playerReportData.nrOfTrashDisposed }
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
            return " Success";
        }
        else
        {
            return " Failed";
        }
    }

    //TODO: VERY TEMP, REDO
    private string GetIncome()
    {
        if (ProgressBar.Instance.GetSlideValue() <= 80)
        {
            return "0";
        }

        return String.Format("{0:0.0}",
            ProgressBar.Instance.GetSlideValue()
            + TimerCountdown.Instance.GetRemainingTime()
            + playerReportData.GetTotalTaskNumber()
            + playerReportData.GetTheSuccessfulMinigameNumber()
            - playerReportData.GetTheFailedMinigameNumber());
    }





}
