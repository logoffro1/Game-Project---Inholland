using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using Photon.Pun;
using Photon.Realtime;


public class PauseMenu : MonoBehaviourPunCallbacks
{
    //Declaring variables
    //isPaused is globally accessible for checking of the game is paused from any other class
    public static bool isPaused = false;
    //These are the UI elements that are hidden/shown when opening the pause menu or the how to play pop-up
    public GameObject pauseMenuUI;
    public GameObject centerDotUI;
    public GameObject hoverTextUI;
    public GameObject howToPlayUI;
    public GameObject howToPlayBtn;
    public GameObject returnToOfficeBtn;
    public GameObject returntoMainMenuBtn;
    public GameObject volumeUI;
    public GameObject volumeButton;

    private bool isInOffice;

    // Start is called before the first frame update
    void Start()
    {
        PhotonNetwork.EnableCloseConnection = true;
         isInOffice = SceneManager.GetActiveScene().name == "NewOffice";
    }

    // Update is called once per frame
    void Update()
    {
        //Checks if escape is pressed to bring up or close the pause menu,
        //and ensures that you cannot pause in the middle of a minigame
        if (!isInOffice) {
            if (MiniGameManager.Instance.IsPlaying == false) {
                HandlePauseButton();
            }
        }
        else
        {
            HandlePauseButton();
        } 
    }

    void HandlePauseButton()
    {
        if (Input.GetKeyUp(KeyCode.Escape))
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

    //This method resumes the game and hides the pause menu,
    //it also unlocks the mouse,
    //and closes the how to play panel in the case that it was open.
    public void Resume()
    {
        GameObject[] players = GameObject.FindGameObjectsWithTag("Player");
        foreach (GameObject p in players)
        {
            p.GetComponentInChildren<Canvas>().enabled = true;
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
        CloseVolumeUI();
        isPaused = false;
    }
    //This method is the opposite of resume, it pauses the game,
    //ensures that only the player that paused has the pause menu open
    //and hides UI elements that obstruct the pause menu
    void Pause()
    {
        GameObject[] players = GameObject.FindGameObjectsWithTag("Player");
        foreach (GameObject p in players)
        {
            p.GetComponentInChildren<Canvas>().enabled = false;
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

    //In the pause menu, there is a how to play button which brings up a popup that displays more information,
    //this hides the pause menu elements that obstruct it
    public void HowToPlay()
    {
        howToPlayUI.SetActive(true);
        howToPlayBtn.SetActive(false);
        returnToOfficeBtn.SetActive(false);
        returntoMainMenuBtn.SetActive(false);
        volumeButton.SetActive(false);
    }

    //This undoes what the HowToPlay() method does
    public void CloseHowToPlay()
    {
        howToPlayUI.SetActive(false);
        howToPlayBtn.SetActive(true);
        returnToOfficeBtn.SetActive(true);
        returntoMainMenuBtn.SetActive(true);
        volumeButton.SetActive(true);
    }
    public void OpenVolumeUI()
    {
        volumeUI.SetActive(true);
        howToPlayBtn.SetActive(false);
        returnToOfficeBtn.SetActive(false);
        returntoMainMenuBtn.SetActive(false);
        volumeButton.SetActive(false);
    }
    public void CloseVolumeUI()
    {
        volumeUI.SetActive(false);
        howToPlayBtn.SetActive(true);
        returnToOfficeBtn.SetActive(true);
        returntoMainMenuBtn.SetActive(true);
        volumeButton.SetActive(true);
    }

    //Return to office button that switches scenes
    public void ReturnToOffice()
    {

        if (PhotonNetwork.IsMasterClient)
        {
            
            LevelManager.Instance.LoadScenePhoton("NewOffice", true);
        }
            
    }

    //Return to Main Menu button that switches scenes
    public void LoadMenu()
    {
        if (PhotonNetwork.IsMasterClient)
        {
            LevelManager.Instance.LoadScenePhoton("MainMenu", true);
        }
        else
        {
            LevelManager.Instance.LoadScenePhoton("MainMenu", false);
        }
    }

    public override void OnLeftRoom()
    {
        Debug.Log("Leaving room...");
    }

    public override void OnPlayerLeftRoom(Photon.Realtime.Player otherPlayer)
    {

    }
  


    //Exit game button, disconnects clients and players,
    //as well as disconnected the session and closing the game
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
