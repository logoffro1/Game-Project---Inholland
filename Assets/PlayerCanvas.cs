using UnityEngine;
using Photon.Pun;

public class PlayerCanvas : MonoBehaviourPun
{
    private Canvas playerCanvas;
    void Start()
    {
        playerCanvas = GetComponent<Canvas>();

        //enable canvas only for current player
        if (photonView.IsMine)
            playerCanvas.enabled = true;
    }
}
