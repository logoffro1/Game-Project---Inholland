using UnityEngine;
using TMPro;
using System;
using System.Linq;
using UnityEngine.Localization;
using UnityEngine.Localization.Settings;
using UnityEngine.Localization.Components;
using System.Collections;

public class MiniGameBase : MonoBehaviour
{
    protected int startingSustainabilityPoints = 5;
    public int StartingSustainabilityPoints { get { return startingSustainabilityPoints; } protected set { startingSustainabilityPoints = value; } }
    public int SustainabilityPoints { get { return startingSustainabilityPoints + (int)pointsOffset; } }

    protected string description;
    public bool IsPlaying { get; set; } = true;
    public TextMeshProUGUI successText;
    public TextMeshProUGUI descriptionText;
    public GameObject infoCanvas; // canvas regarding HOW TO PLAY info

    public MiniGameDifficulty gameDifficulty;

    //Level 
    [Range(0.0f, 100.0f)]
    private float level;
    public float Level { get { return level;  } private set { level = value; } }
    [Range(0.0f, 100.0f)]
    private float levelOffset;
    private float pointsOffset;

    //Tutorial panel
    public GameObject TutorialCanvas;
    public float WaitTime;

    //localized string
    [SerializeField] protected LocalizeStringEvent localizedStringEventDescription;
    [SerializeField] protected LocalizedString localizedStringHint;

    [SerializeField] protected LocalizeStringEvent localizedStringEventResult;
    [SerializeField] protected LocalizedString[] resultLocalizedString;

    private LocalizationSettings locSettings;
    public async void SetLocalizedString()
    {
        try
        {
            //get localization settings (if they exist)
            var handle = LocalizationSettings.InitializationOperation;
            await handle.Task;
            locSettings = handle.Result;
            //set the text to the correct language
            this.description = locSettings.GetStringDatabase().GetLocalizedString(localizedStringHint.TableReference, localizedStringHint.TableEntryReference);

            this.localizedStringEventDescription.StringReference = localizedStringHint;
            this.localizedStringEventDescription.OnUpdateString.AddListener(OnStringChanged);

        }
        catch (Exception ex) //it gets here if localizedString is not set
        {
            Debug.Log(ex.ToString());
        }
    }
    protected virtual void OnStringChanged(string s)// set the hint box based on the minigame and on the language
    {
        if (locSettings == null) return;
        this.description = locSettings.GetStringDatabase().GetLocalizedString(localizedStringHint.TableReference, localizedStringHint.TableEntryReference);
    }

    private void Awake()
    {
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }

    public IEnumerator ShowTutorialCanvas()
    {
        CanvasGroup canvasGroup = TutorialCanvas.GetComponent<CanvasGroup>();
        canvasGroup.alpha = 0.4f;

        yield return new WaitForSeconds(1f);

        float subtractAmount = 0.005f;
        while (canvasGroup.alpha > 0)
        {
            if (!IsPlaying) yield break;

            canvasGroup.alpha -= subtractAmount;
            subtractAmount += subtractAmount / 8;

            yield return null;
        }
    }

    public event Action<InteractableTaskObject> OnGameWon;
    public event Action<InteractableTaskObject> OnGameOver;
    public void Update()
    {
        //Only show the HINT box while pressing H
        if (Input.GetKeyDown(KeyCode.H))
        {
            ShowInfo(true);
        }
        if (Input.GetKeyUp(KeyCode.H))
        {
            ShowInfo(false);
        }
    }
    private void ShowInfo(bool show) // show/hide the hint box
    {
        if (infoCanvas.activeSelf == show) return;

        descriptionText.text = description;
        infoCanvas.SetActive(show);
    }
    protected void GameOver()
    { //stop the game
        MiniGameManager.Instance.GameOver();

        StartCoroutine(MiniGameManager.Instance.StopGame(gameObject));
        ChangeSuccessText(false);
        IsPlaying = false;
        Cursor.lockState = CursorLockMode.Locked; // lock cursor
        ProgressBar.Instance.ChangeSustainibility(-DeterMineSustainibilityPoints(), true); // decrease sustainability
    }
    //Sets the sustainibility point amount for mini games.
    private  float DeterMineSustainibilityPoints()
    {
        switch (gameDifficulty)
        {
            case MiniGameDifficulty.Hard:
                return 6f;
                break;
            case MiniGameDifficulty.Medium:
                return 3.5f;
                break;
            default:
                return 2f;
        }
    }
    protected void GameWon()
    {
        MiniGameManager.Instance.GameWon();

        ChangeSuccessText(true);
        StartCoroutine(MiniGameManager.Instance.StopGame(gameObject));
        IsPlaying = false;
        Cursor.lockState = CursorLockMode.Locked;
        ProgressBar.Instance.ChangeSustainibility(DeterMineSustainibilityPoints(), true);
        TimerCountdown.Instance.SecondsLeft += GetAddedTime();
    }
    private void ChangeSuccessText(bool successful) // show FAILURE / SUCCESS depending on the outcome of the game
    {
        LocalizedString locStr;
        successText.enabled = true;
        if (successful)
        {
            successText.color = Color.green;
            locStr = resultLocalizedString[0];

        }
        else
        {
            successText.color = Color.red;

            locStr = resultLocalizedString[1];
        }

        localizedStringEventResult.StringReference = locStr;
        
        // set the text in the correct language
        successText.text = locSettings.GetStringDatabase().GetLocalizedString(locStr.TableReference, locStr.TableEntryReference);
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
        levelOffset = upgrade.LevelOffSet;
    }

    private void UpdatePointOffset()
    {
        OneOffUpgrade upgrade = FindObjectOfType<Player>().OneOffUpgradeList.Where(x => x.Upgrade == OneOffUpgradesEnum.MinigamePointsIncrease).FirstOrDefault();
        pointsOffset = upgrade.PointsOffSet;
    }

    private int GetAddedTime()
    {
        return FindObjectOfType<Player>().OneOffUpgradeList.Where(x => x.Upgrade == OneOffUpgradesEnum.AddedTimeAfterMinigame).FirstOrDefault().TimeAddAfterMiniGame;
    }


    public virtual void DetermineGameDifficulty() { }

    public virtual void CoordinateLevel() { }
    public virtual void GameFinish(bool success) { }
}

