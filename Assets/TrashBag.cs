using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrashBag : MonoBehaviour
{
    private List<Trash> items;
    private int bagCapacity = 3;

    // Start is called before the first frame update
    void Start()
    {
        items = new List<Trash>();
    }


    public void AddTrash(Trash trash)
    {
        items.Add(trash);
        Debug.Log($"Items in bag: {items.Count}");
    }
    private void EmptyBag()
    {
        items = new List<Trash>();
    }
    public bool CanCollect() => items.Count < bagCapacity;
}
