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


    private void Start()
    {
        IsInteractable = true;
        officeLevels = GameObject.FindObjectOfType<OfficeLevelSelector>();
        SetLocalizedString();
    }

    public override void DoAction(GameObject player)
    {
        officeLevels.ShowPanel(true);
        GameObject.FindObjectOfType<MouseLook>().canRotate = false;
        GameObject.FindObjectOfType<PlayerMovement>().canMove = false;
        UIManager.Instance.ChangeCanvasShown();

    }
}
