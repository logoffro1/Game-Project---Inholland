using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Localization;
using UnityEngine.Localization.Settings;

public class OfficeTV : InteractableObject
{
    private OfficeLevelSelector officeLevels;

    private void Start()
    {
        Locale loc = LocalizationSettings.SelectedLocale;
        LocaleIdentifier localCode = loc.Identifier;
        if (localCode=="en")
        {
            hoverName = "Interact";

        }else if (localCode == "nl")
        {
            hoverName = "Interactie";
        }
        IsInteractable = true;
        officeLevels = GameObject.FindObjectOfType<OfficeLevelSelector>();
    }
    public override void DoAction(GameObject player)
    {
        officeLevels.ShowPanel(true);
        GameObject.FindObjectOfType<MouseLook>().canRotate = false;
        GameObject.FindObjectOfType<PlayerMovement>().canMove = false;
        UIManager.Instance.ChangeCanvasShown();

    }
}
