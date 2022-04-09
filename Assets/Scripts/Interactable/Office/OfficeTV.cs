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

    [SerializeField] private RenderObjects normalRenderer;

    //localized string
    public LocalizeStringEvent localizedStringEvent;
    private void Start()
    {
        localizedStringEvent.OnUpdateString.AddListener(OnStringChanged);
        hoverName = localizedStringEvent.StringReference.GetLocalizedString();

        IsInteractable = true;
        officeLevels = GameObject.FindObjectOfType<OfficeLevelSelector>();
    }

    void OnStringChanged(string s)
    {
        hoverName = localizedStringEvent.StringReference.GetLocalizedString();
    }
    public override void DoAction(GameObject player)
    {
        officeLevels.ShowPanel(true);
        GameObject.FindObjectOfType<MouseLook>().canRotate = false;
        GameObject.FindObjectOfType<PlayerMovement>().canMove = false;
        UIManager.Instance.ChangeCanvasShown();

    }
}
