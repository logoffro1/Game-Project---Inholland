using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Localization.Components;
using Photon.Pun;

public class TrashSpawner : MonoBehaviour
{
    public GameObject[] trashList;
    private int amount = 0;
    [SerializeField] private int limit = 200;
    [SerializeField] private LocalizeStringEvent localizedStringEvent;
    // Start is called before the first frame update
    void Start()
    {
        if(PhotonNetwork.IsMasterClient)
            StartCoroutine(SpawnRoutine());
    }
    private IEnumerator SpawnRoutine() //not really efficient, but it works
    {
        while (amount < limit)
        {
            Vector3 spawnPos = new Vector3(Random.Range(-150f, 150f), 2f, Random.Range(-110f,65f));

            if (DetectCollisions(spawnPos) > 0)
                continue;

            GameObject trash = trashList[Random.Range(0, trashList.Length)];

            GameObject spawnedTrash = PhotonNetwork.Instantiate(trash.name, spawnPos, trash.transform.rotation);
            spawnedTrash.GetComponent<Trash>().SetLocalizedString(localizedStringEvent);
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
