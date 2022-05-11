using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
public class AchievementManager : MonoBehaviour
{
    public List<Achievement> Achievements { get; private set; }
    void Start()
    {
        DontDestroyOnLoad(gameObject);

        PlayerPrefs.DeleteAll(); //DEVELOPMENT ONLY

        Achievements = new List<Achievement>();

        Achievement pressE = new Achievement("Key Pusher!", "Press the E key 10 times!", 10);
        Achievements.Add(pressE);
    }
    private void PressE(Achievement achievement)
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            //achievement.OnIncreaseValue(achievement);
        }
    }

}
