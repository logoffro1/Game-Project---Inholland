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
        Debug.Log("Picked up");
        Destroy(gameObject);
    }
}
