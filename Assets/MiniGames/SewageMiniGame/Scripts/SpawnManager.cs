using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class SpawnManager : MonoBehaviour
{
    [SerializeField] private GameObject[] toxicPrefab;

    private float xSpawn = 1.5f;
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(SpawnLoop());
    }

    // Update is called once per frame
    void Update()
    {

    }
    private void SpawnToxic()
    {
        GameObject go = toxicPrefab[Random.Range(0, toxicPrefab.Length)];
        Vector3 spawnPos = new Vector3(xSpawn, Random.Range(-0.5f, 0.17f), 300f);
        Instantiate(go, spawnPos, go.transform.rotation, transform);
    }
    private IEnumerator SpawnLoop()
    {
        while (true)
        {
            SpawnToxic();
            yield return new WaitForSeconds(Random.Range(0.5f, 2f));
        }
    }

}
