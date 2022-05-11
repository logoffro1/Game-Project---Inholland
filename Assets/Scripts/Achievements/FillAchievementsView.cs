using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FillAchievementsView : MonoBehaviour
{
    private List<Achievement> achievements;
    [SerializeField] private GameObject content;
    [SerializeField] private GameObject achievementInfoPrefab;
    // Start is called before the first frame update
    void Start()
    {
        achievements = FindObjectOfType<GlobalAchievements>().GetAllAchievements();
        foreach(Achievement ach in achievements)
        {
            GameObject achInfo = Instantiate(achievementInfoPrefab,content.transform);
            achInfo.GetComponent<AchievementInfo>().SetInfo(ach.Title, ach.Description, ach.CurrentCount, ach.TriggerCount);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
