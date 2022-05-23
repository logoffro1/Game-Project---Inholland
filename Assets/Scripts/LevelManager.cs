using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System.Threading.Tasks;
using Photon.Pun;
using ExitGames.Client.Photon;
using Photon.Realtime;
using System;

public class LevelManager : MonoBehaviourPun
{
    public static LevelManager Instance;

    [SerializeField] private GameObject loadCanvas;
    [SerializeField] private Slider progressBar;

    private const byte LOADING_PROGRESS_EVENT = 0;

    private float target;
    private string sceneName;
    private void Awake()
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
        PhotonNetwork.NetworkingClient.EventReceived += NetworkingClient_EventReceived;
    }

    private void NetworkingClient_EventReceived(EventData obj)
    {
        if (obj.Code == LOADING_PROGRESS_EVENT)
        {
            Debug.Log("EVENT CODE: " + obj.Code);
            StartCoroutine(LoadingProgress());
        }
    }

    public void LoadScenePhoton(string sceneName, bool loadForAllPlayers)
    {
        this.sceneName = sceneName;
        if (loadForAllPlayers)
        {


            RaiseEventOptions eventOptions = new RaiseEventOptions { Receivers = ReceiverGroup.All };
            bool sent = PhotonNetwork.RaiseEvent(LOADING_PROGRESS_EVENT, null, eventOptions, SendOptions.SendReliable);


        }
        else
        {
            CoroutineLoading();
        }
        if (PhotonNetwork.IsMasterClient)
            PhotonNetwork.LoadLevel(sceneName);




        // var scene = SceneManager.LoadSceneAsync(sceneName);
        // scene.allowSceneActivation = false;
        // scene.allowSceneActivation = true;
    }
    public void LoadSceneSP()
    {
        CoroutineLoading();
        SceneManager.LoadScene(sceneName);
    }
    public void CoroutineLoading()
    {
        StartCoroutine(LoadingProgress());
    }
    private IEnumerator LoadingProgress()
    {         
        target = 0;
        loadCanvas.SetActive(true);
        InitProgressBar();
        while (target < 1f)
        {
            target += UnityEngine.Random.Range(0.2f, 0.5f);
            yield return new WaitForSeconds(UnityEngine.Random.Range(0.2f, 0.5f));
        }
        yield return new WaitForSeconds(1f);
        loadCanvas.SetActive(false);
    }
    private void Update()
    {
        progressBar.value = Mathf.MoveTowards(progressBar.value, target, 3 * Time.deltaTime);
    }
}
