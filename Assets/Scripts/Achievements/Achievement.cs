using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Achievement
{
    public string Title { get; private set; }
    public string Description { get; private set; }
    private int currentCount;
    public int CurrentCount
    {
        get { return currentCount; }

        set
        {
            currentCount = value;
            if (currentCount > TriggerCount)
                currentCount = TriggerCount;
            PlayerPrefs.SetInt(Title + "_count", currentCount);

        }
    }
    public int TriggerCount { get; set; }
    public int AchCode { get; set; }

    public Achievement(string title, string description, int triggerCount)
    {
        this.Title = title;
        this.Description = description;
        this.TriggerCount = triggerCount;

        if (PlayerPrefs.HasKey(Title + "_count"))
        {
            CurrentCount = PlayerPrefs.GetInt(Title + "_count", CurrentCount);
        }
        else
        {
            CurrentCount = 0;
        }
    }
}
