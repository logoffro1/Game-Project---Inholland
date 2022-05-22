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
            // set & save current achievement count
            currentCount = value;
            if (currentCount > TriggerCount)
                currentCount = TriggerCount;
            PlayerPrefs.SetInt(Title + "_count", currentCount);
        }
    }
    public int TriggerCount { get; set; } // when reached, achievement unlocked
    public int AchCode { get; set; } // achievement code ("12345" = achievement unlocked)

    public Achievement(string title, string description, int triggerCount) // achievement constructor
    {
        this.Title = title;
        this.Description = description;
        this.TriggerCount = triggerCount;

        // if exists get count from memory
        if (PlayerPrefs.HasKey(Title + "_count"))
            CurrentCount = PlayerPrefs.GetInt(Title + "_count", CurrentCount);
        else
            CurrentCount = 0;
    }
}
