using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class OfficeLevelSelector : MonoBehaviour
{
    public GameObject LevelsPanel;
    private bool isPanelActive = false;

    public void ShowPanel(bool show)
    {
        isPanelActive = !isPanelActive;
        LevelsPanel.SetActive(show);

        if (show)
        {
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;

        }
        else
        {
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
            GameObject.FindObjectOfType<MouseLook>().canRotate = true;
            GameObject.FindObjectOfType<PlayerMovement>().canMove = true;
            UIManager.Instance.ChangeCanvasShown();

        }
    }
}
