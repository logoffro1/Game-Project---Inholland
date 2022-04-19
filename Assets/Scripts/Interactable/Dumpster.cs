using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Localization;
using UnityEngine.Localization.Settings;

public class Dumpster : InteractableObject
{
    private void Start()
    {


            hoverName = "Empty Trash Bag";

    }
    public override void DoAction(GameObject player)
    {
        if (player.transform.parent.TryGetComponent(out TrashBag trashBag))
        {
            trashBag.EmptyBag();
        }
    }
}
