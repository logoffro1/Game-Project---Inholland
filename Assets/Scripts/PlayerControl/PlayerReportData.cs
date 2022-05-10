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
    public int nrOfTrashDisposed { get; private set; }
   
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
        nrOfTrashDisposed = 0;
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
/*        Debug.Log($"{totalDistance} is the current distance travelled");*/
    }


    public void IncreaseTheNumberOfTrashDisposed(int nr)
    {
        this.nrOfTrashDisposed += nr;
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
    public float calculateIncreaseAmount(int remainingSeconds ,int nrOfHardGames, int nrOfMediumGames, int nrOfEasyGames, bool dayFailed)
    {
        float increaseAmount = 2.7f;
        if (!dayFailed) {

            Debug.Log($"Remaining seconds: {(float)remainingSeconds / 30f}");
            if (remainingSeconds > 60)
            {
                increaseAmount += 1f;
            }
            else
            {
                increaseAmount += (float)remainingSeconds / 300f;
            }
            Debug.Log($"Easy buff: {(float)nrOfEasyGames / 20f}");
            increaseAmount += (float)nrOfEasyGames / 20f;
            Debug.Log($"Medium buff: {(float)nrOfMediumGames / 10f}");
            increaseAmount += (float)nrOfMediumGames / 10f;
            Debug.Log($"Hard buff: {(float)nrOfHardGames / 5f}");
            increaseAmount += (float)nrOfHardGames / 5f;
        } 
        Debug.Log($"Before adjustment of 5, the amount is: {increaseAmount}");

        if (increaseAmount > 5f)
        {
            increaseAmount = 5f;
        }
        Debug.Log($"final amount: {increaseAmount}");
        return increaseAmount;
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
    public int GetEasyGameNumbers()
    {
        int totalNumber = 0;
        foreach (KeyValuePair<string, int> entry in NrOfTasksSuccess)
        {
            if (returnDifficulty(entry.Key) == MiniGameDifficulty.Easy)
                totalNumber += entry.Value;
        }
        return totalNumber;
    }
    public int GetMediumGameNumbers()
    {
        int totalNumber = 0;
        foreach (KeyValuePair<string, int> entry in NrOfTasksSuccess)
        {
            if (returnDifficulty(entry.Key) == MiniGameDifficulty.Medium)
                totalNumber += entry.Value;
        }
        return totalNumber;
    }
    public int GetHardGameNumbers()
    {
        int totalNumber = 0;
        foreach (KeyValuePair<string, int> entry in NrOfTasksSuccess)
        {
            if(returnDifficulty(entry.Key)==MiniGameDifficulty.Hard)
            totalNumber += entry.Value;
        }
        return totalNumber;
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

    public string returnPrefabTaskName(string prefabname)
    {
        string value = "";
        switch (prefabname)
        {
            case "SewageMiniGame":
                value= "Clean sewers";
                break;

            case "RewireMiniGame":
                value= "Rewiring Street lamp";
                break;
            case "SolarShingleGamePrefab":
                value= "Setting up solar panel";
                break;
            case "DigTime Variant":
                value= "Plant trees";
                break;
            case "ColorBeepMiniGame Variant":
                value = "Converting street lamp to solar lamp";
                break;
            case "RecycleGame":
                value = "Recycling the waste";
                break;
            default:
                value = "None";
                break;
        }
        return value;
    }
    //Add calculateDifficulty methods later on
    private MiniGameDifficulty returnDifficulty(string prefabname)
    {
        MiniGameDifficulty value;
        switch (prefabname)
        {
            case "SewageMiniGame":
                value = MiniGameDifficulty.Hard;
                break;
            case "RecycleGame":
            value = MiniGameDifficulty.Hard;
                break;

            case "RewireMiniGame":
                value = MiniGameDifficulty.Easy;
                break;
            case "SolarShingleGamePrefab":
                value = MiniGameDifficulty.Hard;
                break;
            case "DigTime Variant":
                value = MiniGameDifficulty.Easy;
                break;
            case "ColorBeepMiniGame Variant":
                value = MiniGameDifficulty.Medium;
                break;
            default:
                value = MiniGameDifficulty.Medium;
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
