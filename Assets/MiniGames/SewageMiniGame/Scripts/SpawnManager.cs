using System.Collections;
using UnityEngine;
public class SpawnManager : MonoBehaviour // spawn trash
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
    private void SpawnToxic() // spawn trash prefab
    {
        GameObject go = toxicPrefab[Random.Range(0, toxicPrefab.Length)]; // get random trash
        Vector3 spawnPos = new Vector3(xSpawn, Random.Range(-0.5f, 0.17f), transform.parent.position.z); // set random position
       GameObject trash = Instantiate(go, spawnPos, go.transform.rotation, transform); // spawn
        trash.GetComponent<Collectible>().ChangeCoridorSpeed(corridorSpeed); // set trash speed
    }

    private IEnumerator SpawnLoop() // continous spawn
    {
        while (true)
        {
            SpawnToxic();
            yield return new WaitForSeconds(Random.Range(minSeconds, maxSeconds));
        }
    }

}
