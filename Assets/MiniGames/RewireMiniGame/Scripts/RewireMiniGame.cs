using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Localization;
using UnityEngine.Localization.Settings;

public class RewireMiniGame : MiniGameBase
{
    private void Start()
    {
        
        Locale loc = LocalizationSettings.SelectedLocale;
        LocaleIdentifier localCode = loc.Identifier;
        if (localCode == "en")
        {
            description = "Match the wires by color!\n\nClick on the wire on the left side\nDrag to move\nRelease click to connect";

        }
        else if (localCode == "nl")
        {

            description = "Match de draden op kleur!\n\nKlik op de draad aan de linkerkant\nSleep om te bewegen\nLaat de klik los om verbinding te maken ";
        }
    }
    public void GameFinish(bool success)
    {
        if (success) GameWon();
        else GameOver();
    }
}
