using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class ButtonHandler : MonoBehaviour
{
    public void LoadOfficeScene()
    {
        Time.timeScale = 1.0f;
/*        GameObject[] players = GameObject.FindGameObjectsWithTag("Player");
        foreach (GameObject p in players)
        {
            MouseLook m = p.transform.GetComponentInChildren<MouseLook>();
            m.canR = true;

        }*/
        if (PhotonNetwork.IsMasterClient)
        {
            LevelManager.Instance.LoadScenePhoton("Office", true);
        }

    }
}
