using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using Photon.Pun;
using Photon.Realtime;

public class PauseMenu : MonoBehaviourPunCallbacks
{

    public static bool isPaused = false;
    public GameObject pauseMenuUI;
    public GameObject centerDotUI;
    public GameObject hoverTextUI;
    public GameObject howToPlayUI;
    public GameObject howToPlayBtn;
    public GameObject returnToOfficeBtn;
    public GameObject returntoMainMenuBtn;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyUp(KeyCode.Escape) && MiniGameManager.Instance.IsPlaying == false)
        {
            if (isPaused)
            {
                Resume();
            }
            else
            {
                Pause();
            }
        }
    }
    public void Resume()
    {
        GameObject[] players = GameObject.FindGameObjectsWithTag("Player");
        foreach (GameObject p in players)
        {
            if (p.GetComponent<Player>().photonView.IsMine)
            {
                p.GetComponentInChildren<MouseLook>().canR = true;
                break;
            }
        }
        pauseMenuUI.SetActive(false);
        Time.timeScale = 1f;
        centerDotUI.SetActive(true);
        hoverTextUI.SetActive(true);
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        CloseHowToPlay();
        isPaused = false;
    }
    void Pause()
    {
        GameObject[] players = GameObject.FindGameObjectsWithTag("Player");
        foreach (GameObject p in players)
        {
            if (p.GetComponent<Player>().photonView.IsMine)
            {
                p.GetComponentInChildren<MouseLook>().canR = false;
                break;
            }
        }
        pauseMenuUI.SetActive(true);
        //Time.timeScale = 0f;
        centerDotUI.SetActive(false);
        hoverTextUI.SetActive(false);
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
        isPaused = true;
    }

    public void HowToPlay()
    {
        howToPlayUI.SetActive(true);
        howToPlayBtn.SetActive(false);
        returnToOfficeBtn.SetActive(false);
        returntoMainMenuBtn.SetActive(false);
    }

    public void CloseHowToPlay()
    {
        howToPlayUI.SetActive(false);
        howToPlayBtn.SetActive(true);
        returnToOfficeBtn.SetActive(true);
        returntoMainMenuBtn.SetActive(true);
    }

    public void ReturnToOffice()
    {

        if (PhotonNetwork.IsMasterClient)
        {
            
            LevelManager.Instance.LoadScenePhoton("NewOffice", true);
        }
            
    }

    public void LoadMenu()
    {
       // PhotonNetwork.LeaveRoom(true);
        //LevelManager.Instance.LoadScenePhoton("MainMenu",false);
    }
    public override void OnLeftRoom()
    {
        Debug.Log("Leaving room...");
    }
    public override void OnPlayerLeftRoom(Photon.Realtime.Player otherPlayer)
    {

    }



    public void ExitGame()
    {
        if (PhotonNetwork.IsMasterClient)
        {
            foreach(KeyValuePair<int,Photon.Realtime.Player> p in PhotonNetwork.CurrentRoom.Players)
            {
                PhotonNetwork.CloseConnection(p.Value);
                PhotonNetwork.SendAllOutgoingCommands();
            }
        }
        Application.Quit();
    }
}
