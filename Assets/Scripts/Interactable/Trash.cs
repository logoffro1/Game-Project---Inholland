using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Localization;
using UnityEngine.Localization.Settings;

public class Trash : InteractableObject
{
    public bool collected { get; set; } = false;
    private void Awake()
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
