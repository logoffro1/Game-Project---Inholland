using UnityEngine;
using TMPro;
using System;
using System.Linq;
using System.Threading;
using UnityEngine.Localization;
using UnityEngine.Localization.Settings;

public class MiniGameBase : MonoBehaviour
{
    protected int startingSustainabilityPoints = 5;
    public int StartingSustainabilityPoints { get { return startingSustainabilityPoints; } protected set { startingSustainabilityPoints = value; } }
    public int SustainabilityPoints { get { return startingSustainabilityPoints + (int)pointsOffset; } }

    protected string description;
    public bool IsPlaying { get; set; } = true;
    public TextMeshProUGUI successText;
    public TextMeshProUGUI descriptionText;
    public GameObject infoCanvas;

    //Level 
    [Range(0.0f, 100.0f)]
    private float level;
    public float Level { get { return level;  } private set { level = value; } }
    [Range(0.0f, 100.0f)]
    private float levelOffset;
    private float pointsOffset;


    private void Awake()
    {
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }

    public event Action<InteractableTaskObject> OnGameWon;
    public event Action<InteractableTaskObject> OnGameOver;
    public void Update()
    {
        if (Input.GetKeyDown(KeyCode.H))
        {
            ShowInfo(true);
        }
        if (Input.GetKeyUp(KeyCode.H))
        {
            ShowInfo(false);
        }
    }
    private void ShowInfo(bool show)
    {
        if (infoCanvas.activeSelf == show) return;
        CursorLockMode mode = Cursor.lockState;
        bool cursorVisible = Cursor.visible;
        if (show)
        {

            Time.timeScale = 0f;
        }
        else
        {
            Time.timeScale = 1f;
        }
        descriptionText.text = description;
        infoCanvas.SetActive(show);
    }
    protected void GameOver() //remove the duplicate
    {
        MiniGameManager.Instance.GameOver();

        StartCoroutine(MiniGameManager.Instance.StopGame(gameObject));
        ChangeSuccessText(false);
        IsPlaying = false;
        Cursor.lockState = CursorLockMode.Locked;
        ProgressBar.Instance.ChangeSustainibility(-SustainabilityPoints, true);
    }
    protected void GameWon() //remove the duplicate
    {
        MiniGameManager.Instance.GameWon();

        ChangeSuccessText(true);
        StartCoroutine(MiniGameManager.Instance.StopGame(gameObject));
        IsPlaying = false;
        Cursor.lockState = CursorLockMode.Locked;
        ProgressBar.Instance.ChangeSustainibility(SustainabilityPoints, true);
        TimerCountdown.Instance.SecondsLeft += GetAddedTime();
    }
    private void ChangeSuccessText(bool successful)
    {

        successText.enabled = true;
        if (successful)
        {
            successText.color = Color.green;
            successText.text = "SUCCESS";

            return;
        }

        successText.color = Color.red;

            successText.text = "FAILURE";
    }

    public void SetLevel()
    {
        //Level is 90% determined by the sustainability bar, and 10% deterined by time
        float timeRatio = 0.1f;
        float sustainRatio = 0.9f;
        float secondsPercentage = ((float)TimerCountdown.Instance.SecondsMax - TimerCountdown.Instance.SecondsLeft) / 100f;
        float level = (secondsPercentage * timeRatio) + (ProgressBar.Instance.GetSlideValue() * sustainRatio); 

        float minLevel = 20f; //20%
        float maxLevel = 90f;
        float relativeLevel = (maxLevel - minLevel) * level / 100 + 30f;

        UpdateLevelOffset();
        UpdatePointOffset();
        relativeLevel += levelOffset;

        if (relativeLevel > 100) relativeLevel = 100;
        if (relativeLevel < 0) relativeLevel = 0;
        this.Level = relativeLevel;
        CoordinateLevel();
    }

    private void UpdateLevelOffset()
    {
        OneOffUpgrade upgrade = FindObjectOfType<Player>().OneOffUpgradeList.Where(x => x.Upgrade == OneOffUpgradesEnum.MinigameDifficultyDecrease).FirstOrDefault();
        levelOffset += upgrade.LevelOffSet;
    }

    private void UpdatePointOffset()
    {
        OneOffUpgrade upgrade = FindObjectOfType<Player>().OneOffUpgradeList.Where(x => x.Upgrade == OneOffUpgradesEnum.MinigamePointsIncrease).FirstOrDefault();
        pointsOffset += upgrade.PointsOffSet;
    }

    private int GetAddedTime()
    {
        return FindObjectOfType<Player>().OneOffUpgradeList.Where(x => x.Upgrade == OneOffUpgradesEnum.AddedTimeAfterMinigame).FirstOrDefault().TimeAddAfterMiniGame;
    }


    public virtual void CoordinateLevel() { }
    public virtual void GameFinish(bool success) { }
}

