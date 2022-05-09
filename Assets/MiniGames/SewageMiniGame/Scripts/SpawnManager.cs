using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class SpawnManager : MonoBehaviour
{
    [SerializeField] private GameObject[] toxicPrefab;

    private float xSpawn = 1.5f;
    private float minSeconds = 0.5f;
    private float maxSeconds = 2f;

    private float corridorSpeed = 0.35f;

    void Start()
    {
        StartCoroutine(SpawnLoop());
    }
    public void SetCorridorSpeed(float corridorSpeed)
    {
        this.corridorSpeed = corridorSpeed;
    }
    private void SpawnToxic()
    {
        GameObject go = toxicPrefab[Random.Range(0, toxicPrefab.Length)];
        Vector3 spawnPos = new Vector3(xSpawn, Random.Range(-0.5f, 0.17f), transform.parent.position.z);
       GameObject trash = Instantiate(go, spawnPos, go.transform.rotation, transform);
        trash.GetComponent<Collectible>().ChangeCoridorSpeed(corridorSpeed);
    }

    private IEnumerator SpawnLoop()
    {
        while (true)
        {
            SpawnToxic();
            yield return new WaitForSeconds(Random.Range(minSeconds, maxSeconds));
        }
    }

}
