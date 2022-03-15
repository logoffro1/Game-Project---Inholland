using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MiniGameManager : MonoBehaviour
{
    private static MiniGameManager _instance = null;
    public static MiniGameManager Instance { get { return _instance; } }
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
        if (Input.GetKeyDown(KeyCode.T))
        {
            StartCoroutine(StopGame());
        }

    }
    public void StartGame(GameObject miniGamePrefab)
    {
        if (IsPlaying) return;

        UIManager.Instance.ChangeCanvasShown();
        miniGame = Instantiate(miniGamePrefab, new Vector3(0, 0, 100), miniGamePrefab.transform.rotation);
        IsPlaying = true;     
    }
    public IEnumerator StopGame()
    {
        yield return new WaitForSeconds(.5f);
        Destroy(miniGame);
        IsPlaying = false;
        UIManager.Instance.ChangeCanvasShown();
    }
}
