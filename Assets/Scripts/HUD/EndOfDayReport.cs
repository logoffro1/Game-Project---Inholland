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
    private bool dayFailed = false;
    private string lang = "en";
    void Start()
    {
        GetLanguage();
        dayFailed = getWinLoseCondition();
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

        MostPlayedMinigame.text += $"  {playerReportData.GetTheMostPlayedMiniGameName(out playNr)} : {playNr} times";
        timeBonus.text += getSecondsRemainingText();
        DayCondition.text += $"{getWinLoseText()} ";

        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;

    }
    private async void GetLanguage()
    {
        var handle = LocalizationSettings.InitializationOperation;
        await handle.Task;
        LocalizationSettings locSettings = handle.Result;

        lang = locSettings.GetSelectedLocale().Identifier.Code;
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
    private string getSecondsRemainingText()
    {
        int remainingTime = TimerCountdown.Instance.GetRemainingTime();
        switch (lang)
        {
            case "en":
                return $"  {remainingTime} seconds remaining ";
            case "nl":
                return $"  {remainingTime} seconden resterend ";
            case "ro":
                return $"  {remainingTime} secunde ramase ";
            default:
                return $"  {remainingTime} seconds remaining ";
        }
    }
    private string getWinLoseText()
    {

        if (!dayFailed)
        {
            switch (lang)
            {
                case "en":
                    return "Day is sucessfully finished";
                case "nl":
                    return "Dag is succesvol afgesloten";
                case "ro":
                    return "Ziua a fost reusita";
                default:
                    return "Day successful";
            }
        }
        switch (lang)
        {
            case "en":
                return "Day failed";
            case "nl":
                return "Dag is mislukt";
            case "ro":
                return "Ziua a fost esuata";

            default:
                return "Day failed.";
        }
    }


    private bool getWinLoseCondition()
    {
        return !(ProgressBar.Instance.GetSlideValue() >= 80f);

    }




}
