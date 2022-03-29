using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;

public class WireSpawner : MonoBehaviour
{
    public GameObject wirePrefab;
    public Color[] colors;
    private float spawnX = 100;
    private float spawnYRange = 100;
    public int amountWires = 1;

    public int amountFinished;
    public int amountCorrect;

    private List<GameObject> wires;

    public event Action<bool> GameSuccess;

    public RewireMiniGame rewireMiniGame;


    // Start is called before the first frame update
    void Start()
    {
        SetUpGame();

        List<Vector3> spawnPositions = CreateSpawnPositoin();

        InstansiateAllWires(spawnPositions);

        GameSuccess += rewireMiniGame.GameFinish;
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
        List<Color> allColors = colors.ToList();

        for (int i = 0; i < amountWires; i++)
        {
            int posIndex = UnityEngine.Random.Range(0, spawnPositions.Count);
            int colorIndex = UnityEngine.Random.Range(0, allColors.Count);
            Color color = allColors[colorIndex];

            wires.Add(SpawnWire(color, spawnPositions[posIndex]));

            spawnPositions.RemoveAt(posIndex);
            allColors.RemoveAt(colorIndex);
        }

        MoveEndWirePart(spawnPositionsEndPoint);
    }

    private void MoveEndWirePart(List<Vector3> spawnPositionsEndPoint)
    {
        foreach (GameObject wire in wires)
        {
            //Changing the position of the EndWire and EndBackground
            int posIndex = UnityEngine.Random.Range(0, spawnPositionsEndPoint.Count);
            Vector3 spawnPos = spawnPositionsEndPoint[posIndex];
            spawnPos.x = spawnX;

            wire.transform.Find("EndForeground").transform.position = spawnPos;
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
    }

    public void OneIsSuccessFul()
    {
        amountCorrect++;

        if (amountFinished >= amountWires)
        {
            if (amountCorrect >= amountWires)
            {
                GameSuccess?.Invoke(true);
            }
            else
            {
                GameSuccess?.Invoke(false);
            }
        }
    }

    public void OneFailed()
    {
        GameSuccess?.Invoke(false);
    }

    private GameObject SpawnWire(Color color, Vector3 spawnPos)
    {
        GameObject wire = Instantiate(wirePrefab, spawnPos, wirePrefab.transform.rotation, transform);
        wire.GetComponent<Wire>().color = color;

        return wire;
    }
}
