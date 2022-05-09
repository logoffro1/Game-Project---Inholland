using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class ButtonHandler : MonoBehaviour
{
    public void LoadOfficeScene()
    {
        Time.timeScale = 1.0f;
        if (PhotonNetwork.IsMasterClient)
        {
             LevelManager.Instance.LoadScenePhoton("Office",true);
        }
    }
}
