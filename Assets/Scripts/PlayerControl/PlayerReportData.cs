using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerReportData : MonoBehaviour
{

    //In the future, amount of distance traveled and tracking of mission upgrades has to be done. After 7th of April.

    //Value of the progress bar when the day is over.
    public float finalProgress { get; private set; }

    //Number of the tasks the player successfully did.
    public int nrOfTasksSuccess { get; private set; }

    //Number of tasks the player failed.
    public int nrOfTasksFail { get; private set; }

    public string mostPlayedMinigame { get; private set; }

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
