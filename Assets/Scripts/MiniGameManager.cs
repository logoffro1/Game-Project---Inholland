using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MiniGameManager : MonoBehaviour
{
    private static MiniGameManager _instance = null;
    public static MiniGameManager Instance { get { return _instance; } }

    public PlayerReportData PlayerData { get; private set; }

    private PlayFabManager playFabManager;

    public InteractableTaskObject InteractableObject;
    public event Action<InteractableTaskObject> OnGameWon;
    public event Action<InteractableTaskObject> OnGameOver;

    public bool IsPlaying { get; private set; }
    private int miniGameTime;
    private GameObject miniGame;
    public GameObject miniGameScreen;
    private void Awake() // singleton
    {
        if (_instance != null && _instance != this)
            Destroy(this.gameObject);
        else
            _instance = this;
    }

    private Dictionary<string, int> amountOfGameOccurence;
    private int maxOccurence = 2;

    private void Start()
    {
        playFabManager = FindObjectOfType<PlayFabManager>();
        GameObject[] players = GameObject.FindGameObjectsWithTag("Player");
        foreach (GameObject p in players)
        {
            if (p.GetComponent<Player>().photonView.IsMine)
            {
                PlayerData = p.GetComponent<PlayerReportData>();
                break; 
            }
         }
        TaskGenerator taskGenerator = FindObjectOfType<TaskGenerator>();
        amountOfGameOccurence = new Dictionary<string, int>();
        foreach (GameObject gamePrefab in taskGenerator.GamePrefabs)
        {
            amountOfGameOccurence.Add(gamePrefab.name, 0);
        }
    }

    public void StartGame(GameObject miniGamePrefab)
    {
        if (IsPlaying) return;
        miniGameTime = TimerCountdown.Instance.GetRemainingTime();
        PlayerData.AddPlayedGames(miniGamePrefab);
        UIManager.Instance.TurnOnCanvas(false);
        miniGame = Instantiate(miniGamePrefab, new Vector3(0, 0, 1000), miniGamePrefab.transform.rotation);
        MiniGameBase miniGameBase = miniGame.GetComponent<MiniGameBase>();
        miniGameBase.SetLevel();

        amountOfGameOccurence[miniGamePrefab.name]++;

        if (amountOfGameOccurence[miniGamePrefab.name] <= maxOccurence)
        {
            miniGameBase.WaitTime = 2f;
            StartCoroutine(miniGameBase.ShowTutorialCanvas());
        }
        else
        {
            miniGameBase.WaitTime = 0f;
        }

        IsPlaying = true;
    }

    public IEnumerator StopGame(GameObject go)
    {
        miniGameTime -= TimerCountdown.Instance.GetRemainingTime();
        //personalize this later to make it for each mini game.
        playFabManager.WriteCustomPlayerEvent($"{playFabManager.returnPrefabTaskName(this.InteractableObject.GamePrefab.name.ToString())}_FinishedInSeconds", new Dictionary<string, object>
        {
            { miniGame.gameObject.name,miniGameTime}
        });
        yield return new WaitForSeconds(2f);
        MiniGameBase miniGameBase = go.GetComponent<MiniGameBase>();
        Destroy(go);
        IsPlaying = false;
        UIManager.Instance.TurnOnCanvas(true);

        SetAchievements(miniGameBase);
    }
    private void SetAchievements(MiniGameBase miniGameBase) // increases the minigame related achievements
    {
        //task count
        FindObjectOfType<GlobalAchievements>().GetAchievement("taskbeginner").CurrentCount++;
        FindObjectOfType<GlobalAchievements>().GetAchievement("taskenthusiast").CurrentCount++;
        FindObjectOfType<GlobalAchievements>().GetAchievement("taskexpert").CurrentCount++;
        FindObjectOfType<GlobalAchievements>().GetAchievement("taskmaster").CurrentCount++;

        //specific tasks
        if (miniGameBase.GetType() == typeof(SewageMiniGame))
            FindObjectOfType<GlobalAchievements>().GetAchievement("sewage").CurrentCount++;
        else if (miniGameBase.GetType() == typeof(RewireMiniGame))
            FindObjectOfType<GlobalAchievements>().GetAchievement("cable").CurrentCount++;
        else if (miniGameBase.GetType() == typeof(DiggingMiniGame))
            FindObjectOfType<GlobalAchievements>().GetAchievement("gardening").CurrentCount++;
        else if (miniGameBase.GetType() == typeof(ShinglesMiniGame))
            FindObjectOfType<GlobalAchievements>().GetAchievement("solar").CurrentCount++;
        else if (miniGameBase.GetType() == typeof(RecycleMiniGame))
            FindObjectOfType<GlobalAchievements>().GetAchievement("recycle").CurrentCount++;
    }
    public void GameOver()
    {
        OnGameOver?.Invoke(InteractableObject);
        PlayerData.AddLostGames(InteractableObject.GamePrefab);
        FindObjectOfType<GlobalAchievements>().GetAchievement("These are harder than they look").CurrentCount++;
    }

    public void GameWon()
    {
        PlayerData.AddWonGames(InteractableObject.GamePrefab);
        OnGameWon?.Invoke(InteractableObject);
    }

    public void FreezeScreen(bool wantToFreeze)
    {
        UIManager.Instance.TurnOnCanvas(!wantToFreeze);
        IsPlaying = wantToFreeze;
    }
}
