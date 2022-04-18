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
        Debug.Log(" updated from End of the day report.");
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

    public void SendEndOfTheDayReportData(Dictionary<string, string> data)
    {
    var request = new UpdateUserDataRequest
    {
        Data = data
    };
    PlayFabClientAPI.UpdateUserData(request, OnEndOfReportSent, OnError);
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
}
