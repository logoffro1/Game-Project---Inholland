using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NoteSpawner : MonoBehaviour
{
    public GameObject[] NotePrefabs;
    private Vector3 spawnPosition;
    private bool gameFinished = false;
    public float Speed;



    // Start is called before the first frame update
    void Start()
    {
        spawnPosition = transform.position + new Vector3(0, 1, 0);
        StartCoroutine(SpawnNotes());
    }

    private IEnumerator SpawnNotes()
    {
        yield return new WaitForSeconds(2f);

        while (!gameFinished)
        {
            Instantiate(NotePrefabs[Random.Range(0, NotePrefabs.Length)], spawnPosition, transform.rotation, transform);
            yield return new WaitForSeconds(Random.Range(2f, 4f));
        }
    }
}
