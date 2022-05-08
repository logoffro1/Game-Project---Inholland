using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Localization;
using UnityEngine.Localization.Settings;

public class ShinglesMiniGame : MiniGameBase
{
    public AudioSource audioSource;
    public AudioClip winClip;
    public AudioClip loseClip;
    private Board board;
    private void Start()
    {
        audioSource = GetComponent<AudioSource>();
        // description = "Build rows to complete the solar panel!\nKEYS\nA,D-Move left / right\nS-Drop\nSPACE-Hard Drop\nQ,E-Rotate left /right";
        SetLocalizedString();

        //  description = "Bouw twee rijen om het zonnepaneel te voltooien!\n\nKEYS\nA,D - Ga naar links / Rechtsaf\nS - Val\nSPACE - Harde val\nQ,E - Draai naar links / Rechtsaf";

    }


    public override void DetermineGameDifficulty()
    {
        this.gameDifficulty = MiniGameDifficulty.Hard;
    }
    //abstract methods
    public override void CoordinateLevel()
    {
        Debug.Log(((int)this.Level / 14));
        board = gameObject.GetComponentInChildren<Board>();
        board.amountOfLinesNeeded = ((int)this.Level / 14);
        board.amountText.text = $"{board.amountOfLinesNeeded}";
        board.imageFillMaxValue = board.amountOfLinesNeeded;

    }
    public override void GameFinish(bool succesful)
    {

        if (succesful)
        {
            this.GameWon();
            audioSource.PlayOneShot(winClip);

        }
        else
        {
            audioSource.PlayOneShot(loseClip);
            this.GameOver();
        }
    }
}
