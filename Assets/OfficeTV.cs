using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OfficeTV : InteractableObject
{
    private OfficeLevelSelector officeLevels;

    private void Start()
    {
        hoverName = "Open";
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
