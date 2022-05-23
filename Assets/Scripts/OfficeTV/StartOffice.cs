using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

//Instantiates the playerdata when the player first loads the game
public class StartOffice : MonoBehaviourPun
{
    public GameObject PlayerData;
    // Start is called before the first frame update
    void Awake()
    {
        var data = FindObjectOfType<PlayerData>();
        if (data == null)
        {
            PhotonNetwork.Instantiate(PlayerData.name,PlayerData.transform.position,PlayerData.transform.rotation);
        }

        Destroy(this.gameObject);
    }
}
