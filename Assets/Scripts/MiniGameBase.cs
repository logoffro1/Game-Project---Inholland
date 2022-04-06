using UnityEngine;
using TMPro;
using System;
using System.Threading;
using UnityEngine.Localization;
using UnityEngine.Localization.Settings;

public class MiniGameBase : MonoBehaviour
{

    protected int sustainabilityPoints = 10;

    protected string description;

    public bool IsPlaying { get; set; } = true;
    public TextMeshProUGUI successText;
    public TextMeshProUGUI descriptionText;
    public GameObject infoCanvas;
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
        if(Input.GetKeyUp(KeyCode.H))
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
        ProgressBar.Instance.ChangeSustainibility(-sustainabilityPoints,true);
    }
    protected void GameWon() //remove the duplicate
    {
        MiniGameManager.Instance.GameWon();

        ChangeSuccessText(true);
        StartCoroutine(MiniGameManager.Instance.StopGame(gameObject));
        IsPlaying = false;
        Cursor.lockState = CursorLockMode.Locked;
        ProgressBar.Instance.ChangeSustainibility(sustainabilityPoints,true);
    }
    private void ChangeSuccessText(bool successful)
    {
        Locale loc = LocalizationSettings.SelectedLocale;
        LocaleIdentifier localCode = loc.Identifier;
        successText.enabled = true;
        if (successful)
        {
            successText.color = Color.green;
            if (localCode == "en")
            {
                successText.text = "SUCCESS";
            }
            else if (localCode == "nl")
            {
                successText.text = "SUCCESS";
            }           
            return;
        }

        successText.color = Color.red;
        if (localCode == "en")
        {
            successText.text = "FAILURE";
        }
        else if (localCode == "nl")
        {
            successText.text = "MISLUKKING";
        }
        
    }
}

