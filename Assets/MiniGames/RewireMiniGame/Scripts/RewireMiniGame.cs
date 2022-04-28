using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Localization;
using UnityEngine.Localization.Settings;

public class RewireMiniGame : MiniGameBase
{
    private void Start()
    {

        // description = "Match the wires by color!\n\nClick on the wire on the left side\nDrag to move\nRelease click to connect";
        SetLocalizedString();

        //   description = "Match de draden op kleur!\n\nKlik op de draad aan de linkerkant\nSleep om te bewegen\nLaat de klik los om verbinding te maken ";

    }
    public override void GameFinish(bool success)
    {
        if (success) GameWon();
        else GameOver();
    }

    public override void CoordinateLevel()
    {
        int level = Mathf.RoundToInt(this.Level / 10);
        GetComponentInChildren<WireSpawner>().amountWires = level;
    }
}
