using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;

public class UIManager : MonoBehaviour
{
    public TextMeshProUGUI hoverText;
    public TextMeshProUGUI trashText;
    public TextMeshProUGUI flyersText;
    public TextMeshProUGUI countDownText;
    public TextMeshProUGUI startCountDownText;
    public TextMeshProUGUI goalText;
    public GameObject endMissionText;
    public GameObject endOfTheDayReportPrefab;
    public Canvas playerCanvas;
    public GameObject Popup;
    

    public Image trashFillImage;
    //For the countdown
    public Color CountdownBeginningColor;
    public Color CountdownMiddleColor;
    public Color CountdownEndColor;

    private static UIManager _instance = null;
    public static UIManager Instance { get { return _instance; } }
    private Canvas canvas;
    private void Awake() // singleton
    {
        if (_instance != null && _instance != this)
            Destroy(this.gameObject);
        else
            _instance = this;

        canvas = GetComponent<Canvas>();

    }

    private void Start()
    {
        TimerCountdown timerCountdown = GetComponent<TimerCountdown>();
        if(TryGetComponent<TimerCountdown>(out timerCountdown)){
            timerCountdown.OnCountdownEnd += TimerCountdown_OnCountdownEnd;
            timerCountdown.OnSecondChange += TimerCountdown_OnSecondChange;
            timerCountdown.OnStartCountdownChange += TimerCountdown_OnStartCountdownChange;

            //Setting the start of the countdown
            countDownText.text = CountdownString(TimerCountdown.Instance.SecondsMax);
            countDownText.gameObject.SetActive(false);
        }

        MiniGameManager manager = FindObjectOfType<MiniGameManager>();
        if (manager != null) manager.OnGameWon += ShowPopUp;

    }

    // set player inventory
    public void SetPlayerInfo(TextMeshProUGUI trashText, TextMeshProUGUI flyersText, Canvas playerCanvas, Image trashFillImage)
    {
        this.trashText = trashText;
        this.flyersText = flyersText;
        this.playerCanvas = playerCanvas;
        this.trashFillImage = trashFillImage;
    }

    public void SetHoverText(string text) // set the text when hovering over object
    {
        if(text == null)
        {
            hoverText.text = "";
            return;
        }
        hoverText.text = $"(E) {text}";
    }
    public void TurnOnCanvas(bool turnOn)
    {
        canvas.enabled = turnOn;
        playerCanvas.enabled = turnOn;
    }


    public bool IsCanvasEnabled() => canvas.enabled;
    public void SetTrashText(int currentAmount, int limit) // set player trash count
    {
        trashText.text = $"{currentAmount} / {limit}";
        trashFillImage.fillAmount = ((float)currentAmount / (float)limit);
    }
    public void SetFlyersText(int currentAmount, int limit) // set player flyer count
    {
        flyersText.text = $"{currentAmount} / {limit}";
    }
    public void BagFullAnim() // show animation when the trash bag is full
    {
        Animator anim = trashText.gameObject.GetComponent<Animator>();
        anim.SetTrigger("BagFull");
    }
    private void TimerCountdown_OnCountdownEnd(object sender, EventArgs e)
    {
        //Time ended so the progress bar animations has to stop.
        if (ProgressBar.Instance.isGameOngoing)
        {
            ProgressBar.Instance.isGameOngoing = false;
            Instantiate(endOfTheDayReportPrefab);
        }
    }

    private bool FirstSecondPassed = false;

    private void TimerCountdown_OnSecondChange(int countDown)
    {
        if (!FirstSecondPassed)
        {
            countDownText.gameObject.SetActive(true);
            startCountDownText.enabled = false;
            goalText.enabled = false;
            FirstSecondPassed = true;
        }

        if (ProgressBar.Instance.GetSlideValue() == ProgressBar.Instance.GetSliderMaxValue())
        {
            if (ProgressBar.Instance.isGameOngoing) {
                ProgressBar.Instance.isGameOngoing = false;
                Debug.Log("100%run");
                Instantiate(endOfTheDayReportPrefab);
            }      
        }
        countDownText.text = CountdownString(countDown);
        ChangeColor(countDown);
    }

    public string CountdownString(int secondsLeft)
    {
        TimeSpan time = TimeSpan.FromSeconds(secondsLeft);
        return time.ToString(@"mm\:ss");
    }

    private void TimerCountdown_OnStartCountdownChange(string countDown)
    {
        startCountDownText.text = countDown;
    }

    private void ChangeColor(int seconds)
    {
        if (seconds < 60)
        {
            if (IsColorChangeNeeded(CountdownEndColor))
            {
                countDownText.color = CountdownEndColor;
                countDownText.fontSize = countDownText.fontSize++;
                countDownText.gameObject.GetComponentInChildren<RawImage>().color = CountdownEndColor;
            }
        }
        else if (seconds < 120)
        {
            if (IsColorChangeNeeded(CountdownMiddleColor))
            {
                countDownText.color = CountdownMiddleColor;
                countDownText.fontSize = countDownText.fontSize++;
                countDownText.gameObject.GetComponentInChildren<RawImage>().color = CountdownMiddleColor;
            }
        }
        else
        {
            if (IsColorChangeNeeded(CountdownBeginningColor))
            {
                countDownText.color = CountdownBeginningColor;
                countDownText.gameObject.GetComponentInChildren<RawImage>().color = CountdownBeginningColor;
            }
        }

        if (seconds <= 10)
        {
            countDownText.gameObject.GetComponent<CharacterWobble>().enabled = true;
        }
    }

    private bool IsColorChangeNeeded(Color newColor)
    {
        return countDownText.color != newColor;
    }
    public void ShowPopUp(string text)
    {
        GameObject popup = Instantiate(Popup, Popup.transform.position, transform.rotation,transform);
        popup.transform.localPosition = new Vector3(0, 320, 0);
        popup.GetComponent<Popup>().InfoText = text;
    }
    private void ShowPopUp(InteractableTaskObject interactableTaskObject)
    {
        GameObject popup = Instantiate(Popup, Popup.transform.position, transform.rotation, transform);
        popup.transform.localPosition = new Vector3(0, 320, 0);
        popup.GetComponent<Popup>().Task = interactableTaskObject.interactableTaskStatusModels.task;
    }

}
