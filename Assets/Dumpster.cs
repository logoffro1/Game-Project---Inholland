using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dumpster : InteractableObject
{
    private void Start()
    {
        hoverName = "Empty Trash Bag";
    }
    public override void DoAction(GameObject player)
    {
        if(player.transform.parent.TryGetComponent(out TrashBag trashBag))
        {
            trashBag.EmptyBag();
        }
    }
}
