using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrashBag : MonoBehaviour
{
    private List<Trash> items;
    // Start is called before the first frame update
    void Start()
    {
        items = new List<Trash>();
    }

    // Update is called once per frame
    void Update()
    {
        
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
}
