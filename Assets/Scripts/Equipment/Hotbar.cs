using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;

public class Hotbar : MonoBehaviourPunCallbacks
{
    private List<Equipment> equipmentList = new List<Equipment>();
    [SerializeField] private GameObject[] itemSlots;
    [SerializeField] private GameObject playerGameObject;
    private void Start()
    {
        GameObject[] players = GameObject.FindGameObjectsWithTag("Player");
        foreach (GameObject p in players)
        {
            if (p.GetComponent<Player>().photonView.IsMine)
            {
                playerGameObject = p;
                break;
            }
        }
       
        Init();
    }
    private void Init() // initialize the equipment
    {
        equipmentList.Add(GameObject.FindObjectOfType<XrayGoggles>());
        equipmentList.Add(GameObject.FindObjectOfType<Vacuum>());
        equipmentList.Add(GameObject.FindObjectOfType<RunningShoes>());

        foreach(Equipment equipment in equipmentList)
        {
            equipment.onLockedChange += LockItem;
            equipment.onCooldownChange += SetCooldownFill;
            equipment.SetPlayerRep();
        }
    }
    private void LockItem(Equipment item, bool locked) // change item picture to locked / unlocked
    {

        int itemIndex = equipmentList.IndexOf(item);
        GameObject itemLocked = itemSlots[itemIndex].transform.Find("ItemLocked").gameObject;
        GameObject itemCooldown = itemSlots[itemIndex].transform.Find("Cooldown").gameObject;
        if (locked)
        {
            itemLocked.SetActive(true);
            itemCooldown.SetActive(false);
        }
        else
        {
            itemLocked.SetActive(false);
            itemCooldown.SetActive(true);
        }
    }
    private void SetCooldownFill(Equipment item) // set the cooldown fill amount (360)
    {
        int itemIndex = equipmentList.IndexOf(item);
        GameObject itemCooldown = itemSlots[itemIndex].transform.Find("Cooldown").gameObject;
        Image cooldownFill = itemCooldown.transform.Find("CooldownFill").gameObject.GetComponent<Image>();

        cooldownFill.fillAmount = ((float)item.cooldown / (float)item.maxCooldown); // calculate percentage
    }
}
