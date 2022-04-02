using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Trash : InteractableObject
{
    private void Start()
    {
        hoverName = "Trash";
    }
    private void Update()
    {
        if (transform.position.y < -50)
            Destroy(gameObject);
    }
    public override void DoAction(GameObject player)
    {
        if (player.transform.parent.TryGetComponent(out TrashBag bag))
        {
            bag.AddTrash(this);
        }

    }
}
