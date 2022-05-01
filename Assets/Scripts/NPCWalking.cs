using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
public class NPCWalking : MonoBehaviour
{

    [SerializeField] private Transform[] targetPositions;
    private NavMeshAgent navMeshAgent;


    private void Awake()
    {
        navMeshAgent = GetComponent<NavMeshAgent>();
    }
    void Update()
    {
        if (!navMeshAgent.hasPath || Vector3.Distance(navMeshAgent.destination,transform.position) <= 2f)
        {
            HeadToNextWaypoint();
        }
    }
    private void HeadToNextWaypoint()
    {
        if (targetPositions.Length > 0)
            navMeshAgent.destination = targetPositions[Random.Range(0, targetPositions.Length)].position;
    }
}
