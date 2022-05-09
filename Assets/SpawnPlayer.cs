using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon;
public class SpawnPlayer : MonoBehaviourPun
{
    [SerializeField] private GameObject playerPrefab;
    [SerializeField] public Transform[] spawnPositions;
    void Awake()
    {
        Time.timeScale = 1.0f;
        Debug.Log(PhotonNetwork.CurrentRoom.IsOpen);
        if(PhotonNetwork.CurrentRoom.IsOpen)
            PhotonNetwork.Instantiate(playerPrefab.name, spawnPositions[Random.Range(0,spawnPositions.Length)].position, Quaternion.identity);
    }
}
