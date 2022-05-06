using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class TrashBag : MonoBehaviourPun
{
    private List<Trash> items;
    private int bagCapacity = 15;
    PlayerReportData playerData;

    void Start()
    {
        items = new List<Trash>();
        playerData = FindObjectOfType<PlayerReportData>();
    }


    public void AddTrash(Trash trash)
    {
        if (!CanCollect())
        {
            UIManager.Instance.BagFullAnim();
            return;

        }


        items.Add(trash);

        photonView.RPC("ActivateTrash", RpcTarget.AllViaServer, trash.gameObject.GetPhotonView().ViewID, false);

        if (items.Count >= bagCapacity / 2)
        {
            if (Random.Range(0, 11f) == 0)
            {
                BreakBag();
            }
        }
        UIManager.Instance.SetTrashText(items.Count, bagCapacity);
    }
    [PunRPC]
    private void ActivateTrash(int ID, bool active)
    {
        PhotonView.Find(ID).gameObject.SetActive(active);
    }
    public void EmptyBag()
    {
        playerData.IncreaseTheNumberOfTrashDisposed(items.Count);
        StartCoroutine(StartEmptyBag());
    }
    private IEnumerator StartEmptyBag()
    {
        int count = items.Count;
        while (items.Count > 0)
        {
            items.RemoveAt(0);
            Debug.Log(items.Count);
            UIManager.Instance.SetTrashText(items.Count, bagCapacity);
            yield return new WaitForSeconds(0.15f);
        }
        ProgressBar.Instance.ChangeSustainibility(0.5f * count, false);

    }
    private void BreakBag()
    {
        foreach (Trash trash in items)
        {
            GameObject trashGO = trash.gameObject;
            trashGO.SetActive(true);
            trashGO.transform.position = transform.position + transform.forward;
        }
        items = new List<Trash>();

    }
    public bool CanCollect() => items.Count < bagCapacity;
}
