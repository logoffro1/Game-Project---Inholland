using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MiniGameManager : MonoBehaviour
{
    private static MiniGameManager _instance = null;
    public static MiniGameManager Instance { get { return _instance; } }

    //Todo: remove after implementation
    public GameObject tetrisGamePrefab;

    //TODO: might remove
    public InteractableObject InteractableObject;
    public event Action<InteractableObject> OnGameWon;
    public event Action<InteractableObject> OnGameOver;


    public bool IsPlaying { get; private set; }
    private GameObject miniGame;
    public GameObject miniGameScreen;
    private void Awake()
    {
        if (_instance != null && _instance != this)
            Destroy(this.gameObject);
        else
            _instance = this;
    }

    private void Update()
    {
        //if (Input.GetKeyDown(KeyCode.E))
        //{
        //    StartGame();
        //}


        if (Input.GetKeyDown(KeyCode.V))
        {
            UIManager.Instance.ChangeCanvasShown();
            miniGame = Instantiate(tetrisGamePrefab, new Vector3(0, 0, 100), tetrisGamePrefab.transform.rotation);
            IsPlaying = true;
        }

    }

    public void StartGame(GameObject miniGamePrefab)
    {
        if (IsPlaying) return;

        UIManager.Instance.ChangeCanvasShown();
        miniGame = Instantiate(miniGamePrefab, new Vector3(0, 0, 300), miniGamePrefab.transform.rotation);
        IsPlaying = true;     
    }

    public IEnumerator StopGame(GameObject go)
    {
        yield return new WaitForSeconds(2f);
        Destroy(go);
        IsPlaying = false;
        UIManager.Instance.ChangeCanvasShown();
    }

    public void GameOver()
    {
        OnGameOver?.Invoke(InteractableObject);
    }

    public void GameWon()
    {
        OnGameWon?.Invoke(InteractableObject);
    }
}
