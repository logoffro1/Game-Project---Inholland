using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Discord;

public class DiscordController : MonoBehaviour
{
    public Discord.Discord discord;
    public StatusType status;
  

    public string state;
    public string details;
    public string imageKey; //menu , office, farm, citycentre



    private static DiscordController _instance;
    public static DiscordController Instance { get { return _instance; } }
    private void Awake()
    {
        if (_instance != null && _instance != this)
        {
            Destroy(this.gameObject);
        }
        else
        {
            _instance = this;
        }
        DontDestroyOnLoad(this.gameObject);
    }
    void Start()
    {
        discord = new Discord.Discord(972250653097340969, (System.UInt16)Discord.CreateFlags.Default);
        UpdateDiscordStatus(status);
    }
   
    public void UpdateDiscordStatus(StatusType status)
    {
        this.status = status;
        UpdateActivity();
    }
    void UpdateActivity()
    {
        UpdateVariables();
        var activity = new Discord.Activity
        {

            Name = name,
            Details = details,
            State = state,
            Assets = { LargeImage = imageKey }
        };
        var activityManager = discord.GetActivityManager();

        activityManager.UpdateActivity(activity, (res) =>
        {
            if (res == Discord.Result.Ok)
            {
                Debug.Log($"status is updated: {status}");
            }
            else
            {
                Debug.LogError("Discord status failed!!");
            }
        });
    }

    void UpdateVariables()
    {
        switch (status) {

            case StatusType.MainMenu:
                state = "Take your time...";
                details = "Hanging around in main menu";
                imageKey = "menu";
            break;

            case StatusType.Office:
                state = RandomizeOfficeReplies();
                details = "Chilling in the office";
                imageKey = "office";
                break;
            case StatusType.CityCenter:
                state = "Cleaning city center";
                details = "Playing in city centre";
                imageKey = "citycentre";
                break;
            case StatusType.Farm:
                state = "Howdy!";
                details = "Playing in the farm area";
                imageKey = "farm";
                break;
            case StatusType.GameUKDay:
                state = "Running in downtown";
                details = "Alkmaar downtown area";
                imageKey = "citycentre";
                break;
        }
    }

    private string RandomizeOfficeReplies()
    {
        List<string> officereplies = new List<string> { 
            "Playing Chess",
            "Starting the jukebox",
            "Stealing Emre's lunch",
            "Playing beer pong",
            "Getting Rickrolled",
            "Getting scolded by the boss",
            "Sleeping in archive room",
            "Daydreaming about bahamas",
            "Resigning as we speak",
            "On a coffee break",
            "Cleaning the equipments",
            "Making coctails for friday",
        };

            return officereplies[Random.Range(0, officereplies.Count)];
    }

    void Update()
    {
        discord.RunCallbacks();
    }
}
