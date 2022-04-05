using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerReportData : MonoBehaviour
{

    //In the future,tracking of mission upgrades has to be done once that feature is available.

    //Value of the progress bar when the day is over.
    public float FinalProgress { get; private set; }
 
    public Dictionary<string, int> NrOfTasksSuccess { get; private set; }

    public Dictionary<string, int> NrOfTasksFail { get; private set; }

    public Dictionary<string, int> PlayedMinigames { get; private set; }


    [SerializeField] public float totalDistance { get; private set; }

    private Vector3 previousLocation;

    public Vector3 startPosition;
   
    //SewageMiniGame
    //ColorBeepMiniGame
    //DigTime Variant
    //SolarShingleGamePrefab
    //RewireMiniGame
    void Start()
    {
        PlayedMinigames = new Dictionary<string, int>();
        NrOfTasksSuccess = new Dictionary<string, int>();
        NrOfTasksFail = new Dictionary<string, int>();
        FinalProgress = 0;
        totalDistance = 0;
        startPosition = gameObject.transform.position;
    }
    void FixedUpdate()
    {
        MeasureDistance();
    }
    void MeasureDistance()
    {
        totalDistance += Vector3.Distance(gameObject.transform.position, previousLocation);
        previousLocation = gameObject.transform.position;
        Debug.Log($"{totalDistance} is the current distance travelled");
    }

    public int GetTheSuccessfulMinigameNumber()
    {
        int totalNumber = 0;
        foreach (KeyValuePair<string, int> entry in NrOfTasksSuccess)
        {
            totalNumber += entry.Value;
        }
        return totalNumber;
    }
    public int GetTheFailedMinigameNumber()
    {
        int totalNumber = 0;
        foreach (KeyValuePair<string, int> entry in NrOfTasksFail)
        {
            totalNumber += entry.Value;
        }
        return totalNumber;
    }

    public int GetTotalTaskNumber()
    {
        int totalNumber = 0;
        foreach (KeyValuePair<string, int> entry in PlayedMinigames)
        {
            totalNumber += entry.Value;
        }
        return totalNumber;
    }

    public string GetTheMostPlayedMiniGameName(out int numberPlayed)
    {
        string mostPlayedMinigame = "";
        int topPlayNumber = 0;
        foreach (KeyValuePair<string, int> entry in PlayedMinigames)
        {
            if (entry.Value > topPlayNumber)
            {
                topPlayNumber = entry.Value;
                mostPlayedMinigame = entry.Key;
            }
        }
        numberPlayed = topPlayNumber;
        return returnPrefabTaskName(mostPlayedMinigame);
    }

    public void AddWonGames(GameObject minigamePrefab)
    {
        if (NrOfTasksSuccess.ContainsKey(minigamePrefab.name.ToString()))
        {
            NrOfTasksSuccess[minigamePrefab.name]++;
            //This log should be deleted before merge.
            Debug.Log($"{minigamePrefab.name} game won current wins : {NrOfTasksSuccess[minigamePrefab.name]}");
        }
        else
        {
            NrOfTasksSuccess.Add(minigamePrefab.name.ToString(), 1);
            //This log should be deleted before merge.
            Debug.Log($"{minigamePrefab.name} added current wins : {NrOfTasksSuccess[minigamePrefab.name]}");
        }
    }

    public void AddLostGames(GameObject minigamePrefab)
    {
        if (NrOfTasksFail.ContainsKey(minigamePrefab.name.ToString()))
        {
            NrOfTasksFail[minigamePrefab.name]++;
            //This log should be deleted before merge.
            Debug.Log($"{minigamePrefab.name} game lost current loses : {NrOfTasksFail[minigamePrefab.name]}");
        }
        else
        {
            NrOfTasksFail.Add(minigamePrefab.name.ToString(), 1);
            //This log should be deleted before merge.
            Debug.Log($"{minigamePrefab.name} added  current loses: {NrOfTasksFail[minigamePrefab.name]}");
        }
    }

    private string returnPrefabTaskName(string prefabname)
    {
        string value = "";
        switch (prefabname)
        {
            case "SewageMiniGame":
                value= "Cleaning Sewage";
                break;

            case "RewireMiniGame":
                value= "Rewiring Street lamp";
                break;
            case "SolarShingleGamePrefab":
                value= "Setting up solar panel";
                break;
            case "DigTime Variant":
                value= "Planting trees";
                break;
            case "ColorBeepMiniGame Variant":
                value = "Converting street lamp to solar lamp";
                break;
            default:
                value = "None";
                break;
        }
        return value;
    }

    public void AddPlayedGames(GameObject minigamePrefab)
    {
        if (PlayedMinigames.ContainsKey(minigamePrefab.name.ToString()))
        {
            PlayedMinigames[minigamePrefab.name]++;
            //This log should be deleted before merge.
            Debug.Log($"{minigamePrefab.name} value : {PlayedMinigames[minigamePrefab.name]}");
        }
        else
        {
            PlayedMinigames.Add(minigamePrefab.name.ToString(), 1);
            //This log should be deleted before merge.
            Debug.Log($"{minigamePrefab.name} added : {PlayedMinigames[minigamePrefab.name]}");
        }
    }
}
