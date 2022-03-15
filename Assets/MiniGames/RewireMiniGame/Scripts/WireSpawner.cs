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
        SetUpGame();

        List<Vector3> spawnPositions = CreateSpawnPositoin();

        InstansiateAllWires(spawnPositions);
    }

    private void SetUpGame()
    {
        //Centralizing cursor and making it appear
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;

        //Setting starts states
        amountFinished = 0;
        amountCorrect = 0;

        wires = new List<GameObject>();
    }

    private List<Vector3> CreateSpawnPositoin()
    {
        List<Vector3> spawnPositions = new List<Vector3>();

        //Determining spawn positions according to how many wires will spawn, and the x & y coords
        for (int i = 1; i <= amountWires; i++)
        {
            spawnPositions.Add(new Vector3(0, (spawnYRange / amountWires) * i, 0));
        }

        return spawnPositions;
    }

    private void InstansiateAllWires(List<Vector3> spawnPositions)
    {
        List<Vector3> spawnPositionsEndPoint = new List<Vector3>(spawnPositions);

        for (int i = 0; i < amountWires; i++)
        {
            int posIndex = Random.Range(0, spawnPositions.Count);
            wires.Add(SpawnWire(i, spawnPositions[posIndex]));
            spawnPositions.RemoveAt(posIndex);
        }

        MoveEndWirePart(spawnPositionsEndPoint);
    }

    private void MoveEndWirePart(List<Vector3> spawnPositionsEndPoint)
    {
        foreach (GameObject wire in wires)
        {
            //Changing the position of the EndWire and EndBackground
            int posIndex = Random.Range(0, spawnPositionsEndPoint.Count);
            Vector3 spawnPos = spawnPositionsEndPoint[posIndex];
            spawnPos.x = spawnX;

            wire.transform.Find("EndWire").transform.position = spawnPos;
            wire.transform.Find("EndBackground").transform.position = spawnPos;
            spawnPositionsEndPoint.RemoveAt(posIndex);

            //Resetting achoredPosition3DWire because of bug
            var achoredPosition3DWire = wire.GetComponent<RectTransform>().anchoredPosition3D;
            achoredPosition3DWire.z = 0;
            achoredPosition3DWire.x -= 40;
            achoredPosition3DWire.y -= 60;
            wire.GetComponent<RectTransform>().anchoredPosition3D = achoredPosition3DWire;
        }
    }

    public void OneIsFinished()
    {
        amountFinished++;

        if (amountFinished >= amountWires)
        {
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

    private GameObject SpawnWire(int colorIndex, Vector3 spawnPos)
    {
        GameObject wire = Instantiate(wirePrefab, spawnPos, wirePrefab.transform.rotation, transform);
        wire.GetComponent<Wire>().color = colors[colorIndex];

        return wire;
    }
}
