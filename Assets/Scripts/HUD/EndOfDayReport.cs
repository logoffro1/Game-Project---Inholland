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


    private PlayerReportData playerReportData;
    
    void Start()
    {
        //This is temporary. Multiplayer implementation will change it.
        playerReportData = FindObjectOfType<PlayerReportData>();
        DistanceTraveled.text += $"  {(playerReportData.totalDistance-(Math.Abs(playerReportData.startPosition.x))).ToString("F2")} meters";
        //Will be changed in the future. Just for display purposes
        PlayerName.text += "  Jim Morrison";
        int playNr;
        MostPlayedMinigame.text +=$"  {playerReportData.GetTheMostPlayedMiniGameName(out playNr)} / {playNr} times.";
        Success.text += $"  {playerReportData.GetTheSuccessfulMinigameNumber()}";
        Fail.text += $"  {playerReportData.GetTheFailedMinigameNumber()}";
        SliderValue.text += $"  {ProgressBar.Instance.GetSlideValue().ToString("F2")}%";
        TotalTasknumber.text += $"  {playerReportData.GetTotalTaskNumber()}";
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
