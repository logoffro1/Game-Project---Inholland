using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Localization;
using UnityEngine.Localization.Settings;

public class RewireMiniGame : MiniGameBase
{
    private void Start()
    {
        SetLocalizedString();
    }
    public override void GameFinish(bool success)
    {
        if (success) GameWon();
        else GameOver();
    }

    public override void DetermineGameDifficulty()
    {
        this.gameDifficulty = MiniGameDifficulty.Easy;
    }

    //Sets the level of the game
    public override void CoordinateLevel()
    {
        int level = Mathf.RoundToInt(this.Level / 10);
        GetComponentInChildren<WireSpawner>().amountWires = level;
    }
}
