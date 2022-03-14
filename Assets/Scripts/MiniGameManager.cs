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

    public GameObject RewireGamePrefab;
    private GameObject RewireMiniGame;

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

        if (Input.GetKeyDown(KeyCode.P))
        {
            RewireMiniGame = Instantiate(RewireGamePrefab, transform.position + (transform.forward * 2), transform.rotation);
            //miniGameScreen.SetActive(true);
            IsPlaying = true;
            UIManager.Instance.ChangeCanvasShown();
        }

    }
    public void StartGame(GameObject miniGamePrefab)
    {
        /*
        RewireMiniGame = Instantiate(miniGamePrefab, new Vector3(0, 0, 100), RewireGamePrefab.transform.rotation);
        miniGameScreen.SetActive(true);
        IsPlaying = true;
        UIManager.Instance.ChangeCanvasShown();
        */

        
        SewageMiniGame = Instantiate(miniGamePrefab, new Vector3(0, 0, 100), SewageGamePrefab.transform.rotation);
        miniGameScreen.SetActive(true);
        IsPlaying = true;
        UIManager.Instance.ChangeCanvasShown();
        
    }
    private IEnumerator StopGame()
    {
        yield return new WaitForSeconds(.5f);
        Destroy(SewageMiniGame);
        //Destroy(RewireMiniGame);
        miniGameScreen.SetActive(false);
        IsPlaying = false;

        UIManager.Instance.ChangeCanvasShown();
    }
}
