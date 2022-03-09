using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MiniGameManager : MonoBehaviour
{
    private static MiniGameManager _instance = null;
    public static MiniGameManager Instance { get { return _instance; } }
    public bool IsPlaying { get; private set; }
    public GameObject SewageGamePrefab;
    private GameObject SewageMiniGame;
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
        if (Input.GetKeyDown(KeyCode.E))
        {
            StartGame();
        }
        else if (Input.GetKeyDown(KeyCode.T))
        {
            StartCoroutine(StopGame());
        }

    }
    private void StartGame()
    {
        SewageMiniGame = Instantiate(SewageGamePrefab, new Vector3(0, 0, 100), SewageGamePrefab.transform.rotation);
        miniGameScreen.SetActive(true);
        IsPlaying = true;
    }
    private IEnumerator StopGame()
    {
        yield return new WaitForSeconds(1f);
        Destroy(SewageMiniGame);
        miniGameScreen.SetActive(false);
        IsPlaying = false;
    }
}
