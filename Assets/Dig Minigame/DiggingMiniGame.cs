using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Localization;
using UnityEngine.Localization.Settings;

public class DiggingMiniGame : MiniGameBase
{
    private void Start()
    {
        Locale loc = LocalizationSettings.SelectedLocale;
        LocaleIdentifier localCode = loc.Identifier;
        if (localCode == "en")
        {
            description = "Dig a hole for the tree!\nRelease the shovel at the right moment\n\nKEYS\nSPACE - Release shovel";
        }
        else if (localCode == "nl")
        {
            description = "Graaf een gat voor de boom!\nLaat de schop op het juiste moment los\n\nKEYS\nSPACE - Schop loslaten";
        }
        
    }
    public void GameFinish(bool success)
    {
        if (success) GameWon();
        else GameOver();
    }
}
