using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using Photon.Pun;
using ExitGames.Client.Photon;
using Photon.Realtime;
using System;

public class LevelManager : MonoBehaviourPun
{
    public static LevelManager Instance;

    [SerializeField] private GameObject loadCanvas;
    [SerializeField] private Slider progressBar;

    private const byte LOADING_PROGRESS_EVENT = 0; // EVENT CODE (for photon)

    private float target; // progress bar target
    private string sceneName;
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
    private void InitProgressBar()
    {
        target = 0;
        progressBar.value = 0;
        progressBar.minValue = 0;
        progressBar.maxValue = 1;
    }
    private void OnEnable()
    {
        PhotonNetwork.NetworkingClient.EventReceived += NetworkingClient_EventReceived; //subscribe to receive events
    }

    private void NetworkingClient_EventReceived(EventData obj)
    { //if the client received an event check the event code, if it is loading start the loading process
        if (obj.Code == LOADING_PROGRESS_EVENT)
            StartCoroutine(LoadingProgress());
    }

    public void LoadScenePhoton(string sceneName, bool loadForAllPlayers)
    {
        this.sceneName = sceneName;
        if (loadForAllPlayers) //if the scene needs to be loaded for everyone in the lobby
        {
            RaiseEventOptions eventOptions = new RaiseEventOptions { Receivers = ReceiverGroup.All };
            PhotonNetwork.RaiseEvent(LOADING_PROGRESS_EVENT, null, eventOptions, SendOptions.SendReliable); // raise the loading event
        }
        else // if loading only for caller
        {
            CoroutineLoading();
        }
        if (PhotonNetwork.IsMasterClient)
            PhotonNetwork.LoadLevel(sceneName);
    }
    public void LoadSceneSP() // single player loading
    {
        CoroutineLoading();
        SceneManager.LoadScene(sceneName);
    }
    public void CoroutineLoading()
    {
        StartCoroutine(LoadingProgress());
    }
    private IEnumerator LoadingProgress() // show loading screen
    {   
        //Discord status change happens on every scene change before LoadSceneAsync();
        if (DiscordController.Instance.IsDiscordRunning())
        {
            StatusType type = (StatusType)Enum.Parse(typeof(StatusType), this.sceneName);
            DiscordController.Instance.UpdateDiscordStatus(type);
        }

        target = 0;
        loadCanvas.SetActive(true);
        InitProgressBar();
        while (target < 1f) // simulate progress bar loading
        {
            target += UnityEngine.Random.Range(0.2f, 0.5f);
            yield return new WaitForSeconds(UnityEngine.Random.Range(0.2f, 0.5f));
        }
        yield return new WaitForSeconds(1f);
        loadCanvas.SetActive(false);
    }
    private void Update()
    {
        // change progress bar value constantly
        progressBar.value = Mathf.MoveTowards(progressBar.value, target, 3 * Time.deltaTime);
    }
}
