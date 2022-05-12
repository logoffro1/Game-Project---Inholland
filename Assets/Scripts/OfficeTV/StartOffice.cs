using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

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

    // Update is called once per frame
    void Update()
    {
        
    }
}
