using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Discord;
using System;

public class DiscordController : MonoBehaviour
{
    //This class is used for rich presence feature we have in discord. It simply shows what is each individual player is doing in the game: Being in lobby, in farm map etc...
    public Discord.Discord discord;
    public StatusType status;
  

    public string state;
    public string details;
    public string imageKey; //menu , office, farm, citycentre

    private bool isDiscordRunning;



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

    //An essential method to enable/disable the feature to not to enforce the user to have discord open for our game.
    public bool IsDiscordRunning()
    {
         isDiscordRunning = false;
        // loops through open processes
        for (int i = 0; i < System.Diagnostics.Process.GetProcesses().Length; i++)
        {
            // checks if current process is discord
            if (System.Diagnostics.Process.GetProcesses()[i].ToString() == "System.Diagnostics.Process (Discord)")
            {
                isDiscordRunning = true;
                break;
            }
        }
        return isDiscordRunning;
    }

  
    void Start()
    {
        if (IsDiscordRunning())
        {
            discord = new Discord.Discord(972250653097340969, (System.UInt16)Discord.CreateFlags.Default);
            UpdateDiscordStatus(status);
        }

    }
   
    //Changing the status and the activity
    public void UpdateDiscordStatus(StatusType status)
    {
        this.status = status;
        UpdateActivity();
    }
    //Using discord sdk to change the users rich presence with the current data.
    void UpdateActivity()
    {
        try
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
        catch (NullReferenceException exception) {
            isDiscordRunning = false;
            Debug.Log("Discord messed up in update but its ok");
        }
    }
    //Some colorful rich presence data. Note that if this is not liked, it is easily changable. Or it can be added new events for christmas or halloween with fun messages.
    void UpdateVariables()
    {
        switch (status) {

            case StatusType.MainMenu:
                state = "Take your time...";
                details = "Hanging around in main menu";
                imageKey = "menu";
            break;

            case StatusType.NewOffice:
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
            case StatusType.Playground:
                state = "Doing cool stuff";
                details = "Testing in playground";
                imageKey = "office";
                break;
        }
    }
    //Some fun options for the office.
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
            "Making cocktails for friday",
        };
            return officereplies[UnityEngine.Random.Range(0, officereplies.Count)];
    }

    void Update()
    {
        try
        {
            if (isDiscordRunning)
            {
                discord.RunCallbacks();
            }
        }
        catch (NullReferenceException exception)
        {
            isDiscordRunning = false;
            Debug.Log("Discord messed up in update but its ok" + exception.Message);
        }
    }
}
