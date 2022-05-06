using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Discord;

public class DiscordController : MonoBehaviour
{
    public Discord.Discord discord;
    void Start()
    {
        discord = new Discord.Discord(972250653097340969, (System.UInt16)Discord.CreateFlags.Default);
        var activityManager = discord.GetActivityManager();
        var activity = new Discord.Activity {
            
            Details = "I love dew dew",
            State= "Chilling in the office"
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
