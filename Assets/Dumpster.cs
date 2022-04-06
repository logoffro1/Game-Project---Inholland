using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Localization;
using UnityEngine.Localization.Settings;

public class Dumpster : InteractableObject
{
    private void Start()
    {
        Locale loc = LocalizationSettings.SelectedLocale;
        LocaleIdentifier localCode = loc.Identifier;

        if (localCode == "en")
            hoverName = "Empty Trash Bag";
        if (localCode == "nl")
            hoverName = "Lege vuilniszak";
    }
    public override void DoAction(GameObject player)
    {
        if (player.transform.parent.TryGetComponent(out TrashBag trashBag))
        {
            trashBag.EmptyBag();
        }
    }
}
