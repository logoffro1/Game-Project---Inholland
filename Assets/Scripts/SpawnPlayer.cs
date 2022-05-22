using UnityEngine;
using Photon.Pun;
public class SpawnPlayer : MonoBehaviourPun
{
    [SerializeField] private GameObject playerPrefab;
    [SerializeField] public Transform[] spawnPositions;
    void Awake() // Instantiate player on the network
    {
        Time.timeScale = 1.0f; //make sure the time is never stopped at this point
        
        if(PhotonNetwork.CurrentRoom.IsOpen)
            PhotonNetwork.Instantiate(playerPrefab.name, spawnPositions[Random.Range(0,spawnPositions.Length)].position, Quaternion.identity);
    }
}
