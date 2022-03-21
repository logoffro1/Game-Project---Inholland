using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using TMPro;
public class MiniGameBase : MonoBehaviour
{

    public bool IsPlaying { get; set; } = true;
    public TextMeshProUGUI successText;

    protected void GameOver()
    {
        Debug.Log("GAME OVER");
        StartCoroutine(MiniGameManager.Instance.StopGame(gameObject));
        ChangeSuccessText(false);
        IsPlaying = false;
    }
    protected void GameWon()
    {
        ChangeSuccessText(true);
        StartCoroutine(MiniGameManager.Instance.StopGame(gameObject));
        IsPlaying = false;
    }
    private void ChangeSuccessText(bool successful)
    {
        successText.enabled = true;
        if (successful)
        {
            successText.color = Color.green;
            successText.text = "SUCCESS";
            return;
        }
        successText.color = Color.red;
        successText.text = "FAILURE";
    }
}

