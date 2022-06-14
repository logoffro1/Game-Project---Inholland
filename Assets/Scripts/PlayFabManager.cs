using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using PlayFab;
using PlayFab.ClientModels;
using System;

public  class PlayFabManager : MonoBehaviour
{
    // This class helps tracking play fab data and sharing it with developers and stakeholders for development purposes.
    void Start()
    {
        Login();
        /*PlayFabSettings.staticPlayer;*/
    }
    //For logging into play fab
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
    //For leaderboard implementation
    //(if its neededyou can change the minigameswon part with a string variable and have a dynamic method for all leaderboards)
    //This is just a simple implementation to make the game leaderboard scalable for future.
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
    //This is used for almost all of the data tracking of the game. It simply has an event name as a key and a dictionary of collective data that is to be sent to playfab. 
    //Rest is handled by the playfab api
    public void WriteCustomPlayerEvent(string eventName,Dictionary<string,object> playerData)
    {
        PlayFabClientAPI.WritePlayerEvent(new WriteClientPlayerEventRequest
        {
            EventName = eventName,
            Body = playerData
        }, (e) => Debug.Log("Event Successfully recorded"),
        (error) => Debug.Log(error.GenerateErrorReport()));
}
    //Event for getting user data
    public void GetUserData()
    {
        PlayFabClientAPI.GetUserData(new GetUserDataRequest(), OnReceivedData, OnError);
    }
    //Event for receiving user data
    private void OnReceivedData(GetUserDataResult obj)
    {
        Debug.Log("Data Received");
    }
    //Event for sending user data
    private void OnEndOfReportSent(UpdateUserDataResult obj)
    {
        Debug.Log("Data sent from end of day report");
    }

    //This is simply for writing custom event names.
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
