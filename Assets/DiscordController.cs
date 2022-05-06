using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Discord;

public class DiscordController : MonoBehaviour
{
    public Discord.Discord discord;

  
    public string name;

    public string state;

    public string details;
    void Start()
    {
        discord = new Discord.Discord(972250653097340969, (System.UInt16)Discord.CreateFlags.Default);
        var activityManager = discord.GetActivityManager();
        var activity = new Discord.Activity {
            
            Name = name,
            Details = details,
            State= state,
            Assets = { LargeImage = "big"}
        };

        activityManager.UpdateActivity(activity, (res) =>
        {
            if (res == Discord.Result.Ok)
            {
                Debug.Log("Discord status set!");
            }
            else
            {
                Debug.LogError("Discord status failed!!");
            }
        });
    }

    void Update()
    {
        discord.RunCallbacks();
    }
}
