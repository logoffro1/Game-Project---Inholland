using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.Rendering.Universal;
using UnityEngine.Localization;
using UnityEngine.Localization.Components;
using UnityEngine.Localization.Settings;

public class OfficeTV : InteractableObject
{
    private OfficeLevelSelector officeLevels;

    [SerializeField] private LocalizeStringEvent localizeStringEvent;


    private void Start()
    {
        IsInteractable = true;
        officeLevels = GameObject.FindObjectOfType<OfficeLevelSelector>();
        SetLocalizedString(localizeStringEvent);
    }

    public override void DoAction(GameObject player)
    {
        officeLevels.ShowPanel(true);
    }
}
