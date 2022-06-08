using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Localization;
using UnityEngine.Localization.Settings;

public class FillAchievementsView : MonoBehaviour
{
    private List<Achievement> achievements;
    [SerializeField] private GameObject content;
    [SerializeField] private GameObject achievementInfoPrefab;
    [SerializeField] private LocalizedString[] localizedTitles;
    [SerializeField] private LocalizedString[] localizedDescriptions;

    private LocalizationSettings locSettings;
    void Start()
    {
        GetLoc();

        Fill();
    }

    private void Fill()
    {
        // fill the achievements view in the main menu
        achievements = FindObjectOfType<GlobalAchievements>().GetAllAchievements();
        int count = 0;
        foreach (Achievement ach in achievements)
        {
            GameObject achInfo = Instantiate(achievementInfoPrefab, content.transform);
            string title = locSettings.GetStringDatabase().GetLocalizedString(localizedTitles[count].TableReference, localizedTitles[count].TableEntryReference);
            string description = locSettings.GetStringDatabase().GetLocalizedString(localizedDescriptions[count].TableReference, localizedDescriptions[count].TableEntryReference);
            achInfo.GetComponent<AchievementInfo>().SetInfo(title, description, ach.CurrentCount, ach.TriggerCount);
            count++;
        }
    }
    private async void GetLoc()
    {
        //get localization settings (if they exist)
        var handle = LocalizationSettings.InitializationOperation;
        await handle.Task;
        locSettings = handle.Result;
    }
}
