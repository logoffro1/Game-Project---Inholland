using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon;
public class SpawnPlayer : MonoBehaviourPun
{
    [SerializeField] private GameObject playerPrefab;
    // Start is called before the first frame update
    void Start()
    {

        PhotonNetwork.Instantiate(playerPrefab.name, transform.position, Quaternion.identity);
    }
}
