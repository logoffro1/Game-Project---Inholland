using UnityEngine;
using Photon.Pun;

public class PlayerCanvas : MonoBehaviourPun
{
    private Canvas playerCanvas;
    public static PlayerCanvas Instance;
    void Start()
    {
        playerCanvas = GetComponent<Canvas>();

        //enable canvas only for current player
        /* if (!photonView.IsMine)
             DestroyImmediate(gameObject);*/
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
            Destroy(gameObject);
        playerCanvas.enabled = true;
    }
}
