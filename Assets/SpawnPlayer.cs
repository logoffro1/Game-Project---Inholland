using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon;
public class SpawnPlayer : MonoBehaviourPun
{
    [SerializeField] private GameObject playerPrefab;
    [SerializeField] private Transform[] spawnPositions;
    void Awake()
    {

        PhotonNetwork.Instantiate(playerPrefab.name, spawnPositions[Random.Range(0,spawnPositions.Length)].position, Quaternion.identity);
    }
}
