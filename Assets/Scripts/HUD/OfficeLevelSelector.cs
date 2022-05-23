using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class OfficeLevelSelector : MonoBehaviour
{
    public GameObject LevelsPanel;

    public void ShowPanel(bool show)
    {
        CastRay.Instance.CanInteract = !show;

        LevelsPanel.SetActive(show);
        Cursor.visible = show;

        GameObject.FindObjectOfType<MouseLook>().canRotate = !show;
        GameObject.FindObjectOfType<PlayerMovement>().canMove = !show;

        UIManager.Instance.TurnOnCanvas(!show);

        if (show)
        {
            Cursor.lockState = CursorLockMode.None;
        }
        else
        {
            Cursor.lockState = CursorLockMode.Locked;
        }
    }
}
