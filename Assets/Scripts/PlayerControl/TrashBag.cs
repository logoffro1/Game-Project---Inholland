using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrashBag : MonoBehaviour
{
    private List<Trash> items;
    private int bagCapacity = 15;

    void Start()
    {
        items = new List<Trash>();
    }


    public void AddTrash(Trash trash)
    {
        if (!CanCollect())
        {
            UIManager.Instance.BagFullAnim();
            return;

        }
     
        trash.gameObject.SetActive(false);
        items.Add(trash);
        if(items.Count >= bagCapacity / 2)
        {
            if (Random.Range(0, 11f) == 0)
            {
                BreakBag();
            }
        }
        UIManager.Instance.SetTrashText(items.Count, bagCapacity);
    }
    public void EmptyBag()
    {
        StartCoroutine(StartEmptyBag());
    }
    private IEnumerator StartEmptyBag()
    {
        int count = items.Count;
        while(items.Count > 0)
        {
            items.RemoveAt(0);
            Debug.Log(items.Count);
            UIManager.Instance.SetTrashText(items.Count, bagCapacity);
            yield return new WaitForSeconds(0.15f);
        }
        ProgressBar.Instance.ChangeSustainibility(0.5f * count);

    }
    private void BreakBag()
    {
        foreach(Trash trash in items)
        {
            GameObject trashGO = trash.gameObject;
            trashGO.SetActive(true);
            trashGO.transform.position = transform.position + transform.forward;
        }
        items = new List<Trash>();

    }
    public bool CanCollect() => items.Count < bagCapacity;
}
