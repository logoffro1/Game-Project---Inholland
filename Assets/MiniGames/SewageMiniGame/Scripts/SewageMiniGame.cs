using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Localization;
using UnityEngine.Localization.Settings;

public class SewageMiniGame : MiniGameBase //remove the singleton
{
    private int lives = 3;
    private int score = 0;
    private int maxScore = 10;
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

        SetLocalizedString();

    }
    public void DecreaseLives()
    {
        lives -= 1;
        if (lives <= 0)
        {
            lives = 0;
            this.GameOver();
        }

        sewageUIManager.ChangeLifes(lives);
    }
    public void IncreaseScore()
    {
        score += 1;

        if (score >= maxScore)
            this.GameWon();

        sewageUIManager.ChangeScoreText(score, maxScore);
    }
    public override void CoordinateLevel()
    {
        Debug.Log(this.Level);
        float newSpeed = this.Level / 150;
        FindObjectOfType<SpawnManager>().SetCorridorSpeed(newSpeed);
    }
}
