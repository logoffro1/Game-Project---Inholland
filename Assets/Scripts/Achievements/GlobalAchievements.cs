using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GlobalAchievements : MonoBehaviour // manage all achievements
{
    public static GlobalAchievements Instance;
    private AudioSource audioSource;
    private List<Achievement> achievements = new List<Achievement>();

    private void Awake() // persistent singleton
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
            Destroy(gameObject);
    }
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        InitAchievements();
    }
    private void InitAchievements() // create achievements & add to list
    {
        achievements.Add(new Achievement("Stop Littering", "Pick up 100 trash from the ground", 100));
        achievements.Add(new Achievement("The Holy Grail", "Visit the city center church", 1));
        achievements.Add(new Achievement("Alkmaar Explorer", "Play in each region of Alkmaar at least once", 2));
        achievements.Add(new Achievement("The Activist", "Give 30 flyers away", 30));
        achievements.Add(new Achievement("Detour around Alkmaar", "Travel a total of 5km in the city center", 5));
        achievements.Add(new Achievement("Task Beginner", "Complete 10 tasks", 10));
        achievements.Add(new Achievement("Task Enthusiast", "Complete 25 tasks", 25));
        achievements.Add(new Achievement("Task Expert", "Complete 50 tasks", 50));
        achievements.Add(new Achievement("Task Master", "Complete 100 tasks", 100));
        achievements.Add(new Achievement("Wallhacks", "Use the XRay Goggles for the first time", 1));
        achievements.Add(new Achievement("Run Forest, Run!", "Use the Running Shoes for the first time", 1));
        achievements.Add(new Achievement("The cleaning lady", "Use the Vacuum for the first time", 1));
        achievements.Add(new Achievement("These are harder than they look", "Fail 5 tasks", 5));
        achievements.Add(new Achievement("I like the way you recycle, boy!", "Complete 25 recycling tasks", 25));
        achievements.Add(new Achievement("Gardening Simulator", "Complete 25 planting tree tasks", 25));
        achievements.Add(new Achievement("The Cable Guy", "Complete 25 rewire lamp tasks", 25));
        achievements.Add(new Achievement("Sewage Cleaner", "Complete 15 sewage cleaning tasks", 15));
        achievements.Add(new Achievement("How did I even get up there?", "Complete 10 solar panel tasks", 10));
    }
    void Update()
    {
        foreach (Achievement ach in achievements) // loop through all achievements
        {
            if (PlayerPrefs.HasKey(ach.Title)) // if achievement exists in memory
            {
                ach.AchCode = PlayerPrefs.GetInt(ach.Title); // get achievement code from memory
            }
            if (ach.CurrentCount == ach.TriggerCount && ach.AchCode != 12345) // achievement unlocked
            {
                StartCoroutine(TriggerAchievement(ach));
            }
        }

    }
    public Achievement GetAchievement(string title) // get achievement by title
    {
        foreach (Achievement ach in achievements)
        {
            if (ach.Title.Equals(title)) return ach;
        }
        return null;
    }
    public List<Achievement> GetAllAchievements() => achievements;
    private IEnumerator TriggerAchievement(Achievement ach) // achievement unlocked
    {
        ach.AchCode = 12345;
        PlayerPrefs.SetInt(ach.Title, ach.AchCode);
        audioSource.Play();
        if(UIManager.Instance != null)
        {
            UIManager.Instance.ShowPopUp($"Achievement Unlocked: {ach.Title}");
        }
        yield return null;

    }
}
