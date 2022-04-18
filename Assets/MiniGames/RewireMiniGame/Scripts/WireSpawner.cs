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
    public GameObject explosionParticleEffect;

    //The ranges of where the wires can spawn
    private float spawnX = 100;
    private float spawnYRange = 110;

    //Change amount of wires that spawn
    public int amountWires = 1;

    private int amountFinished;
    private int amountCorrect;
    private List<GameObject> wires;

    [HideInInspector]
    public RewireMiniGame rewireMiniGame;

    //Audio
    [HideInInspector]
    public AudioSource audioSource;
    public AudioClip clickAudio;
    public AudioClip successAudio;
    public AudioClip failAudio;

    //Events
    public event Action<bool> GameSuccess;

    // Start is called before the first frame update
    void Start()
    {
        SetUpGame();

        List<Vector3> spawnPositions = CreateSpawnPositoin();
        audioSource = GetComponent<AudioSource>();

        InstansiateAllWires(spawnPositions);

        GameSuccess += rewireMiniGame.GameFinish;
    }

    private void SetUpGame()
    {
        //Setting starts states
        amountFinished = 0;
        amountCorrect = 0;

        wires = new List<GameObject>();
    }

    private List<Vector3> CreateSpawnPositoin()
    {
        List<Vector3> spawnPositions = new List<Vector3>();

        //Determining spawn positions according to how many wires will spawn, and the x & y coords
        int tmpAmount = amountWires - 1;
        if (tmpAmount <= 0) tmpAmount = 1;

        float median = 0;

        for (int i = 0; i <= tmpAmount; i++)
        {
            float positionY = (transform.position.y + (spawnYRange / tmpAmount) * i) * (amountWires * 0.1f);
            spawnPositions.Add(new Vector3(0, positionY, 0));
            median += positionY;
        }

        float middle;

        //if even
        if (spawnPositions.Count % 2 == 0)
        {
            middle = (spawnPositions[spawnPositions.Count / 2].y + spawnPositions[(spawnPositions.Count / 2) -1].y)/2;
        }
        else
        {
            //odd
            middle = spawnPositions[Mathf.CeilToInt(spawnPositions.Count / 2f)].y;

        }

        middle -= 60;

        for (int i = 0; i <= tmpAmount; i++)
        {
            Vector3 currentPos = spawnPositions[i];
            currentPos.y -= middle;
            spawnPositions[i] = currentPos;
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
                audioSource.PlayOneShot(successAudio);
                GameSuccess?.Invoke(true);
            }
            else
            {
                audioSource.PlayOneShot(failAudio);
                GameSuccess?.Invoke(false);
            }
        }
        else
        {
            audioSource.PlayOneShot(clickAudio);
        }
    }

    public void OneFailed()
    {
        audioSource.PlayOneShot(failAudio);
        GameSuccess?.Invoke(false);
    }

    private GameObject SpawnWire(Color color, Vector3 spawnPos)
    {
        GameObject wire = Instantiate(wirePrefab, spawnPos, wirePrefab.transform.rotation, transform);
        wire.GetComponent<Wire>().color = color;

        return wire;
    }

    public void InstantiateExplosion(Vector3 position, Color color)
    {
        //var lightenFactor = 5f;

        var main = explosionParticleEffect.GetComponent<ParticleSystem>().main;
        //color.r += lightenFactor;
        //color.g += lightenFactor;
        //color.b += lightenFactor;

        main.startColor = color;

        Instantiate(explosionParticleEffect, position, explosionParticleEffect.transform.rotation);
    }
}
