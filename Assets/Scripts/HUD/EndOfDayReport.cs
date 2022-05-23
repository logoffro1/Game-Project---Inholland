using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Localization;
using UnityEngine.Localization.Settings;
using UnityEngine.UI;
using Photon.Pun;

public class EndOfDayReport : MonoBehaviour
{
    //This is a class for prompting all the collected data per match to the user. This data is shown separately for each user.

    //Text values for each data type collected
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
    private PlayFabManager playFabManager;
    public AudioClip DayReportAudio;
    public GameObject ReturnLobbyButton;


    //Objects for data tracking
    private PlayerReportData playerReportData;
    private PlayerReputation playerRep;
    private bool dayFailed = false;
    private string lang = "en";

    private PlayerData playerData;

    void Start()
    {
        //Only the lobby leader can return to lobby
        if (PhotonNetwork.IsMasterClient)
        {
            ReturnLobbyButton.SetActive(true);
        }

        GetLanguage();
        GameObject[] players = GameObject.FindGameObjectsWithTag("Player");
        //For all players get the playerReportData object to track their data
        foreach (GameObject p in players)
        {
            if (p.GetComponent<Player>().photonView.IsMine)
            {
                playerReportData = p.GetComponent<PlayerReportData>();
                p.GetComponentInChildren<MouseLook>().canR = false;
                break;
            }
        }

        foreach (PlayerData pd in FindObjectsOfType<PlayerData>())
        {
            if (pd.photonView.IsMine)
            {
                playerData = pd;
            }
        }
        foreach (PlayerReputation pr in FindObjectsOfType<PlayerReputation>())
        {
            if (pr.photonView.IsMine)
            {
                playerRep = pr;
            }
        }
        PlayerReportData[] datas = FindObjectsOfType<PlayerReportData>();
        foreach (PlayerReportData data in datas)
        {
            if (data.photonView.IsMine)
            {
                playerReportData = data;
                data.gameObject.GetComponentInChildren<MouseLook>().canR = false;
                break;
            }
        }
        dayFailed = GetWinCondition();

        //Calculate the exp that is gained for this match
        playerRep.IncreaseEXP(TimerCountdown.Instance.SecondsLeft,
            playerReportData.GetHardGameNumbers(),
            playerReportData.GetMediumGameNumbers(),
            playerReportData.GetEasyGameNumbers(),
            dayFailed);

        //Calculate the current map progress for this match

        playerData.NewSustainabilityPoints =
            playerReportData.calculateIncreaseAmount(
                TimerCountdown.Instance.SecondsLeft,
            playerReportData.GetHardGameNumbers(),
            playerReportData.GetMediumGameNumbers(),
            playerReportData.GetEasyGameNumbers(),
            dayFailed);

        playerData.AddToCurrentDistrict(playerData.NewSustainabilityPoints);
        DontDestroyOnLoad(playerData.gameObject);//Try Playerdata Start instead

        playFabManager = FindObjectOfType<PlayFabManager>();


        //Calculate the data shown to the player and save it to the text objects

        //  playerReportData = FindObjectOfType<PlayerReportData>();
        string distance = (playerReportData.totalDistance - (Math.Abs(playerReportData.startPosition.x))).ToString("F2");
        DistanceTraveled.text += $"{distance} m";
        //achievements
        int distanceM = (int)(playerReportData.totalDistance - Math.Abs(playerReportData.startPosition.x)) / 1000;
        FindObjectOfType<GlobalAchievements>().GetAchievement("detour").CurrentCount += distanceM;

        int playNr;

        Location.text = playerData.IsInDistrictName;
        PlayerName.text = PhotonNetwork.LocalPlayer.NickName;
        Success.text += $"{playerReportData.GetTheSuccessfulMinigameNumber()}";
        Fail.text += $"{playerReportData.GetTheFailedMinigameNumber()}";
        SliderValue.text += $"{ProgressBar.Instance.GetSlideValue().ToString("F2")}%";
        TotalTasknumber.text += $"{playerReportData.GetTotalTaskNumber()}";

        writePlayFabData();
        MostPlayedMinigame.text += $"{playerReportData.GetTheMostPlayedMiniGameName(out playNr)} : {playNr} times";
        timeBonus.text += GetSecondsRemainingText();
        DayCondition.text += $"{GetWinLoseText()} ";

        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
        // Time.timeScale = 0f;

        GetComponent<AudioSource>().PlayOneShot(DayReportAudio);
    }


    private string GetSecondsRemainingText()
    {
        int remainingTime;
        if (playerData.IsInGameMode == GameMode.Chill)
            remainingTime = 0;
        else
            remainingTime = TimerCountdown.Instance.GetRemainingTime();

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
    //This is to determine if the game was won
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
    //This is for the data that goes to playfab
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
}
