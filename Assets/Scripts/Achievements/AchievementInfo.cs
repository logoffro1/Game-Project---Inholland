using UnityEngine;
using TMPro;

public class AchievementInfo : MonoBehaviour // achievment information to show in the achievements section
{
    [SerializeField] private TextMeshProUGUI title;
    [SerializeField] private TextMeshProUGUI description;
    [SerializeField] private TextMeshProUGUI progress;
    public void SetInfo(string title, string description, int currentAmount, int targetAmount)
    {
        this.title.SetText(title);
        this.description.SetText(description);
        this.progress.SetText($"{currentAmount}/{targetAmount}");
    }

}
