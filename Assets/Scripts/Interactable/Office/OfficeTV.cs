using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.Rendering.Universal;
using UnityEngine.Localization;
using UnityEngine.Localization.Settings;

public class OfficeTV : InteractableObject
{
    private OfficeLevelSelector officeLevels;

    [SerializeField] private RenderObjects normalRenderer;

    private void Start()
    {

        hoverName = "Interact";

        IsInteractable = true;
        officeLevels = GameObject.FindObjectOfType<OfficeLevelSelector>();
        normalRenderer.SetActive(true);
    }
    public override void DoAction(GameObject player)
    {
        officeLevels.ShowPanel(true);
        GameObject.FindObjectOfType<MouseLook>().canRotate = false;
        GameObject.FindObjectOfType<PlayerMovement>().canMove = false;
        UIManager.Instance.ChangeCanvasShown();

    }
}
