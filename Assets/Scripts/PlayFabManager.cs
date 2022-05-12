using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using PlayFab;
using PlayFab.ClientModels;
using System;

public  class PlayFabManager : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        Login();
        /*PlayFabSettings.staticPlayer;*/
    }

  void Login()
    {
        var request = new LoginWithCustomIDRequest
        {
            CustomId = SystemInfo.deviceUniqueIdentifier,
            CreateAccount = true
        };
    PlayFabClientAPI.LoginWithCustomID(request, OnSuccess, OnError);
    }
    void OnSuccess(LoginResult result)
    {
        Debug.Log("Logged in");
    }

    void OnError(PlayFabError error)
    {
        Debug.Log("Error while trying the playfab request");
        Debug.Log(error.GenerateErrorReport());
    }

    void OnLeaderBoardSent(UpdatePlayerStatisticsResult result)
    {
    }
   /* void OnEndOfDayReportSuccess(UpdatePlayerStatisticsResult result)
    {
        Debug.Log("End of the day report is sent from End of the day report.");
    }*/
    public void SendLeaderBoard(int score)
    {
        var request = new UpdatePlayerStatisticsRequest()
        {
            Statistics = new List<StatisticUpdate>
            {
                new StatisticUpdate
                {
                    StatisticName= "MinigamesWon",
                    Value = score
                }
            }
        };
        PlayFabClientAPI.UpdatePlayerStatistics(request, OnLeaderBoardSent, OnError);
    }

    public void WriteCustomPlayerEvent(string eventName,Dictionary<string,object> playerData)
    {
        PlayFabClientAPI.WritePlayerEvent(new WriteClientPlayerEventRequest
        {
            EventName = eventName,
            Body = playerData
        }, (e) => Debug.Log("Event Successfully recorded"),
        (error) => Debug.Log(error.GenerateErrorReport()));
}

    public void GetUserData()
    {
        PlayFabClientAPI.GetUserData(new GetUserDataRequest(), OnReceivedData, OnError);
    }

    private void OnReceivedData(GetUserDataResult obj)
    {
        Debug.Log("Data Received");
    }

    private void OnEndOfReportSent(UpdateUserDataResult obj)
    {
        Debug.Log("Data sent from end of day report");
    }

    //This code is an abomination. It has to be eradicated from the face of the earth. It will be gone by beta.
    public string returnPrefabTaskName(string prefabname)
    {
        string value = "";
        switch (prefabname)
        {
            case "SewageMiniGame":
                value = "Clean_sewers";
                break;

            case "RewireMiniGame":
                value = "Rewiring_Game";
                break;
            case "SolarShingleGamePrefab Variant 1":
                value = "Building_Solar_Panel";
                break;
            case "DigTime Variant":
                value = "Digging_Game";
                break;
            case "ColorBeepMiniGame Variant":
                value = "Color_Beep_Game";
                break;
            default:
                value = "None";
                break;
        }
        return value;
    }

}
