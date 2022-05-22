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

    public void AddTrash(Trash trash) // if the player can still collect trash, add it to the bag
    {
        if (!CanCollect())
        {
            UIManager.Instance.BagFullAnim();
            return;

        }
        items.Add(trash);

        //increase achievement
        Achievement achPickupTrash = FindObjectOfType<GlobalAchievements>().GetAchievement("Stop Littering");
        achPickupTrash.CurrentCount++;

        photonView.RPC("ActivateTrash", RpcTarget.AllViaServer, trash.gameObject.GetPhotonView().ViewID, false); // send RPC to all clients

        if (items.Count >= bagCapacity / 2) // the fuller the bag is, the more chances that it will break
        {
            if (Random.Range(0, 12f) == 1)
            {
                BreakBag();
            }
        }
        UIManager.Instance.SetTrashText(items.Count, bagCapacity);
    }
    [PunRPC]
    private void ActivateTrash(int ID, bool active) // set the trash active for the specific client
    {
        PhotonView.Find(ID).gameObject.SetActive(active);
    }
    public void EmptyBag()
    {
        playerData.IncreaseTheNumberOfTrashDisposed(items.Count);
        StartCoroutine(StartEmptyBag());
    }
    private IEnumerator StartEmptyBag() // empty the trash bag over time
    {
        int count = items.Count;
        while (items.Count > 0)
        {
            items.RemoveAt(0);
            UIManager.Instance.SetTrashText(items.Count, bagCapacity);
            yield return new WaitForSeconds(0.15f);
        }
        ProgressBar.Instance.ChangeSustainibility(0.5f * count, false); // change sustainability based on how much trash thrown away

    }
    private void BreakBag() // break the bag and spill all the trash around the player
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
