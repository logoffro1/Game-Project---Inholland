using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrashSpawner : MonoBehaviour
{
    public GameObject[] trashList;
    private int amount = 0;
   [SerializeField] private int limit = 200;
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(SpawnRoutine());   
    }


    private IEnumerator SpawnRoutine() //not really efficient, but it works
    {
        while(amount < limit)
        {
            Vector3 spawnPos = new Vector3(Random.Range(180f, -40f), 0.4f, Random.Range(100f, -60f));

            if (DetectCollisions(spawnPos) > 0)
                continue;

            GameObject trash = trashList[Random.Range(0, trashList.Length)];
            Instantiate(trash, spawnPos, trash.transform.rotation, transform);
            yield return null;
            amount++;
        }
    }
    private int DetectCollisions(Vector3 pos)
    {
        Collider[] hitColliders = Physics.OverlapSphere(pos, 0.5f);
        return hitColliders.Length;
    }
}
