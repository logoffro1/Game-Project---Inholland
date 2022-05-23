using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Hotbar : MonoBehaviour
{
    private List<Equipment> equipmentList = new List<Equipment>();
    [SerializeField] private GameObject[] itemSlots;
    [SerializeField] private GameObject playerGameObject;
    private void Start()
    {
        Init();
    }
    private void Init() // initialize the equipment
    {
        equipmentList.Add(playerGameObject.GetComponentInChildren<XrayGoggles>());
        equipmentList.Add(playerGameObject.GetComponentInChildren<Vacuum>());
        equipmentList.Add(playerGameObject.GetComponentInChildren<RunningShoes>());

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
