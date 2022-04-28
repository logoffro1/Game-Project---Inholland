using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Localization;
using UnityEngine.Localization.Settings;

public class DiggingMiniGame : MiniGameBase
{
    private void Start()
    {

        SetLocalizedString();
        // description = "Dig a hole for the tree!\nRelease the shovel at the right moment\n\nKEYS\nSPACE - Release shovel";

        //description = "Graaf een gat voor de boom!\nLaat de schop op het juiste moment los\n\nKEYS\nSPACE - Schop loslaten";

    }
    public override void GameFinish(bool success)
    {
        if (success) GameWon();
        else GameOver();
    }

    public override void CoordinateLevel()
    {
        MoveLine moveline = gameObject.transform.Find("WholeBar").GetComponentInChildren<MoveLine>();
        moveline.Speed = moveline.StartingSpeed * (this.Level / 50);
    }

}
