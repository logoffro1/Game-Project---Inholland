using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerReportData : MonoBehaviour
{

    //In the future,tracking of mission upgrades has to be done once that feature is available.

    //Value of the progress bar when the day is over.
    public float FinalProgress { get; private set; }
    [SerializeField] private float distanceTravelled;

    //Number of the tasks the player successfully did.
    public Dictionary<string, int> NrOfTasksSuccess { get; private set; }


    //Number of tasks the player failed.
    public Dictionary<string, int> NrOfTasksFail { get; private set; }

    public Dictionary<string, int> PlayedMinigames { get; private set; }


    public float totalDistance = 0;
    private Vector3 previousLocation;
   
    void Start()
    {
        PlayedMinigames = new Dictionary<string, int>();
        FinalProgress = 0;
        NrOfTasksSuccess = new Dictionary<string, int>();
        NrOfTasksFail = new Dictionary<string, int>();
        totalDistance = 0;
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
