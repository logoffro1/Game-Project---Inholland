using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SewageMiniGame : MonoBehaviour //change to event based
{
    private int lives = 3;
    private int score = 0;
    private int maxScore = 15;
    public bool isPlaying { get; set; } = true;
    private static SewageMiniGame _instance = null;
    public static SewageMiniGame Instance { get { return _instance; } }
    private SewageUIManager sewageUIManager;
    private void Awake()
    {
        if (_instance != null && _instance != this)
            Destroy(this.gameObject);
        else
            _instance = this;
    }
    private void Start()
    {
        sewageUIManager = GameObject.FindObjectOfType<SewageUIManager>();
    }
    public void DecreaseLives()
    {
        lives -= 1;
        if (lives <= 0)
        {
            lives = 0;
            GameOver();
        }

        sewageUIManager.ChangeLifes(lives);
    }
    public void IncreaseScore()
    {
        score += 1;

        if(score >= maxScore)
            GameWon();

        sewageUIManager.ChangeScoreText(score,maxScore);
    }
    private void GameOver() //change to events
    {
        sewageUIManager.ChangeSuccessText(false);
        isPlaying = false;
    }
    private void GameWon()
    {
        sewageUIManager.ChangeSuccessText(true);
        Debug.Log("YOU WON");
        isPlaying = false;
    }
}
