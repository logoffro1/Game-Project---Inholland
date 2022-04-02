using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Trash : InteractableObject
{
    private void Start()
    {
        hoverName = "Trash";
    }
    public override void DoAction(GameObject player)
    {
        if (player.transform.parent.TryGetComponent(out TrashBag bag))
        {
            if (!bag.CanCollect()) return;

            bag.AddTrash(this);
            Destroy(gameObject);
        }

    }
}
