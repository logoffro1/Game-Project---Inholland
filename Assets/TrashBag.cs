using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrashBag : MonoBehaviour
{
    private List<Trash> items;
    private int bagCapacity = 15;

    // Start is called before the first frame update
    void Start()
    {
        items = new List<Trash>();
    }


    public void AddTrash(Trash trash)
    {  
        trash.gameObject.SetActive(false);
        items.Add(trash);
        if(items.Count >= bagCapacity / 2)
        {
            if (Random.Range(0, 11f) <= 1)
            {
                BreakBag();
            }
        }

        Debug.Log($"Items in bag: {items.Count}");
    }
    private void EmptyBag()
    {
        items = new List<Trash>();
    }
    private void BreakBag()
    {
        foreach(Trash trash in items)
        {
            Vector3 randomRadius = new Vector3(Random.Range(0.5f, 2.0f), 0, Random.Range(0.5f, 2.0f));

            GameObject trashGO = trash.gameObject;
            trashGO.SetActive(true);
            trashGO.transform.position = transform.position + transform.forward;
        }
        items = new List<Trash>();
    }
    public bool CanCollect() => items.Count < bagCapacity;
}
