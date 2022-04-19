using UnityEngine;
using TMPro;
using System;
using System.Linq;
using System.Threading;
using UnityEngine.Localization;
using UnityEngine.Localization.Settings;

public class MiniGameBase : MonoBehaviour
{

    protected int sustainabilityPoints = 5;
    public int SustainabilityPoints { get { return sustainabilityPoints; } set { sustainabilityPoints = value; } }

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
    public float LevelOffset { get { return levelOffset; } set { levelOffset = value; } }


    private void Awake()
    {
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
        //LevelOffset += FindObjectOfType<Player>().OneOffUpgradeList.Where(x => x.Upgrade == )
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
        ProgressBar.Instance.ChangeSustainibility(-sustainabilityPoints, true);
    }
    protected void GameWon() //remove the duplicate
    {
        MiniGameManager.Instance.GameWon();

        ChangeSuccessText(true);
        StartCoroutine(MiniGameManager.Instance.StopGame(gameObject));
        IsPlaying = false;
        Cursor.lockState = CursorLockMode.Locked;
        ProgressBar.Instance.ChangeSustainibility(sustainabilityPoints, true);
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

        relativeLevel += LevelOffset;

        if (relativeLevel > 100) relativeLevel = 100;
        if (relativeLevel < 0) relativeLevel = 0;
        this.Level = relativeLevel;
        CoordinateLevel();
    }

    public virtual void CoordinateLevel() { }
    public virtual void GameFinish(bool success) { }
}

