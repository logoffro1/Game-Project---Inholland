using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class AchievementInfo : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI title;
    [SerializeField] private TextMeshProUGUI description;
    [SerializeField] private TextMeshProUGUI progress;
    // Start is called before the first frame update
    void Start()
    {
        
    }
    public void SetInfo(string title, string description, int currentAmount, int targetAmount)
    {
        this.title.SetText(title);
        this.description.SetText(description);
        this.progress.SetText($"{currentAmount}/{targetAmount}");
    }

}
