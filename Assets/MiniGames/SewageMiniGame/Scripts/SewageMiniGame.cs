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

            description = "Collect all the trash!\n\nKEYS\nA,D - Move left/right\nSPACE - Shoot hook down";

           // description = "Verzamel al het afval!\n\nToetsen\nA,D - Ga naar links / Rechtsaf\nSPACE - Haak los";

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
        float level = this.level;
        foreach(Collectible collectible in FindObjectsOfType<Collectible>()) //level == 10, (og * 9), level == 90, (og * 1)
        {
            collectible.Speed = collectible.OriginalSpeed * 1000;
        }

        switch (true)
        {
            case var value when level > 90:
                lives = 1;
                break;
            case var value when level > 80:
                lives = 3;
                break;
            case var value when level > 70:
                lives = 4;
                break;
            case var value when level > 60:
                lives = 5;
                break;
            case var value when level > 40:
                lives = 6;
                break;
            case var value when level > 30:
                lives = 7;
                break;
            case var value when level > 20:
                lives = 8;
                break;
            case var value when level > 10:
                lives = 9;
                break;
            case var value when level <  10:
                lives = 10;
                break;
            default:
                break;


        }
    }
}
