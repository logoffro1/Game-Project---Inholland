using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WindTurbineMinigame : MiniGameBase
{
    //General class for controlling the win lose condition, styling and audio effects of the solar wind turbine welding mini game.
    public AudioSource audioSource;
    public AudioClip winClip;
    public AudioClip loseClip;
    public float lineSpeed;
    public int difficultyLevel;
    private WeldingLine line;
    void Start()
    {
        SetLocalizedString();
        line = GetComponentInChildren<WeldingLine>();
        DetermineGameDifficulty();
    }
    public override void DetermineGameDifficulty()
    {
        this.gameDifficulty = MiniGameDifficulty.Medium;
    }
    public override void CoordinateLevel()
    {
        lineSpeed = 0.2f;
        difficultyLevel = 20;
        Debug.Log($"Addition {((int)this.Level / 8)}");
        difficultyLevel += ((int)this.Level / 8);
        lineSpeed +=this.Level / 1000;
        Debug.Log("Speed: "+lineSpeed);
        Debug.Log("Level: "+difficultyLevel);
    }
    public override void GameFinish(bool succesful)
    {
        line.isStarted = false;
        if (succesful)
        {
            this.GameWon();
            audioSource.PlayOneShot(winClip);
        }
        else
        {
            this.GameOver();
            audioSource.PlayOneShot(loseClip);
        }
    }
}
