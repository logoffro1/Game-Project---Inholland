using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
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
        pauseMenuUI.SetActive(true);
        Time.timeScale = 0f;
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
        Time.timeScale = 1f;
        LevelManager.Instance.LoadScene("Office",true);
    }

    public void LoadMenu()
    {
        Time.timeScale = 1f;
        LevelManager.Instance.LoadScene("MainMenu",false);
    }

    public void ExitGame()
    {
        Debug.Log("quitting game");
        Application.Quit();
    }
}
