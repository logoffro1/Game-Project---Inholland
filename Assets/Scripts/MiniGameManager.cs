using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MiniGameManager : MonoBehaviour
{
    private static MiniGameManager _instance = null;
    public static MiniGameManager Instance { get { return _instance; } }

    public PlayerReportData PlayerData { get; private set; }

    //Todo: remove after implementation
    public GameObject tetrisGamePrefab;

    private PlayFabManager playFabManager;

    //TODO: might remove
    public InteractableTaskObject InteractableObject;
    public event Action<InteractableTaskObject> OnGameWon;
    public event Action<InteractableTaskObject> OnGameOver;


    public bool IsPlaying { get; private set; }
    int miniGameTime;
    private GameObject miniGame;
    public GameObject miniGameScreen;
    private void Awake()
    {
        if (_instance != null && _instance != this)
            Destroy(this.gameObject);
        else
            _instance = this;
    }

    private void Start()
    {
        playFabManager = FindObjectOfType<PlayFabManager>();
        PlayerData = FindObjectOfType<PlayerReportData>();
    }

    public void StartGame(GameObject miniGamePrefab)
    {
        if (IsPlaying) return;
        miniGameTime = TimerCountdown.Instance.GetRemainingTime();
        PlayerData.AddPlayedGames(miniGamePrefab);
        UIManager.Instance.ChangeCanvasShown();
        miniGame = Instantiate(miniGamePrefab, new Vector3(0, 0, 300), miniGamePrefab.transform.rotation);
        IsPlaying = true;     
    }

    public IEnumerator StopGame(GameObject go)
    {
        miniGameTime -= TimerCountdown.Instance.GetRemainingTime();
        //personalize this later to make it for each mini game.
        playFabManager.WriteCustomPlayerEvent("Minigame_Task_Completion_Seconds", new Dictionary<string, object>
        {
            { miniGame.gameObject.name,miniGameTime}
        });
        yield return new WaitForSeconds(2f);
        Destroy(go);
        IsPlaying = false;
        UIManager.Instance.ChangeCanvasShown();
    }

    public void GameOver()
    {
        
        PlayerData.AddLostGames(InteractableObject.GamePrefab);
        OnGameOver?.Invoke(InteractableObject);
    }

    public void GameWon()
    {
        PlayerData.AddWonGames(InteractableObject.GamePrefab);
        OnGameWon?.Invoke(InteractableObject);
    }

    public void FreezeScreen(bool wantToFreeze)
    {
        UIManager.Instance.ChangeCanvasShown();
        IsPlaying = wantToFreeze;
    }
}
