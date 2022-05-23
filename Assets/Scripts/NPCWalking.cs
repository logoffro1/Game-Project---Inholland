using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using Photon.Pun;
public class NPCWalking : MonoBehaviourPun
{

    [SerializeField] private Transform[] targetPositions;
    private NavMeshAgent navMeshAgent;
    [SerializeField] private Transform currentWaypoint;


    private void Awake()
    {
        if (!PhotonNetwork.IsMasterClient) return;
        navMeshAgent = GetComponent<NavMeshAgent>();
        HeadToNextWaypoint();
    }
    void Update()
    {
        if (!PhotonNetwork.IsMasterClient) return;
        if (!navMeshAgent.pathPending || Vector3.Distance(navMeshAgent.destination,transform.position) <= 3f)
        {
            HeadToNextWaypoint();
        }
    }
    private void HeadToNextWaypoint()
    {
        if (targetPositions.Length > 0)
        {
            currentWaypoint = targetPositions[Random.Range(0, targetPositions.Length)].transform;
            navMeshAgent.destination = currentWaypoint.position;
            

        }
            
    }
}
