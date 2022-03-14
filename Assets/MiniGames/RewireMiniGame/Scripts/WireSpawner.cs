using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class WireSpawner : MonoBehaviour
{
    /* Notes from Cosmin
     * 1. See notes from Wire.cs & Dragger.cs
     * 2. Research using LineRenderer instead of just stretching the sprites, it will make it cleaner and more scalable
     */
    public GameObject wirePrefab;
    public Color[] colors;
    private float spawnX = 100;
    private float spawnYRange = 100;
    public int amountWires = 1;

    public int amountFinished;
    public int amountCorrect;

    private List<GameObject> wires;

    // Start is called before the first frame update
    void Start()
    {
        //Cursor.lockState = CursorLockMode.None;

        amountFinished = 0;
        amountCorrect = 0;

        wires = new List<GameObject>();
        List<Vector3> spawnPositions = new List<Vector3>();

        for(int i = 1; i <= amountWires; i++)
        {
            spawnPositions.Add(new Vector3(0, (spawnYRange/amountWires)*i, 0));
        }

        List<Vector3> spawnPositionsEndPoint = new List<Vector3>(spawnPositions);

        for (int i = 0; i < amountWires; i++)
        {
            int posIndex = Random.Range(0, spawnPositions.Count);
            wires.Add(SpawnWire(i, spawnPositions[posIndex]));
            spawnPositions.RemoveAt(posIndex);
        }

        foreach(GameObject wire in wires)
        {
            int posIndex = Random.Range(0, spawnPositionsEndPoint.Count);
            Vector3 spawnPos = spawnPositionsEndPoint[posIndex];
            spawnPos.x = spawnX;

            wire.transform.Find("EndWire").transform.position = spawnPos;
            wire.transform.Find("EndBackground").transform.position = spawnPos;
            spawnPositionsEndPoint.RemoveAt(posIndex);
        }

        Cursor.lockState = CursorLockMode.None;
    }


    void Update()
    {
        if (Input.GetKeyDown(KeyCode.R))
        {
            SceneManager.LoadScene("GameUKDay");
        }
    }

    public void OneIsFinished()
    {
        amountFinished++;

        if (amountFinished >= amountWires)
        {
            Time.timeScale = 0f;
            if (amountCorrect >= amountWires)
            {
                Debug.Log("WINNER!");
            }
            else
            {
                Debug.Log("lol loser");
            }
        }
    }

    public void OneIsSuccessFul()
    {
        amountCorrect++;
    }

    GameObject SpawnWire(int colorIndex, Vector3 spawnPos)
    {
        GameObject someObject = Instantiate(wirePrefab, spawnPos, wirePrefab.transform.rotation,transform);
        someObject.GetComponent<Wire>().color = colors[colorIndex];

        return someObject;
    }
}
