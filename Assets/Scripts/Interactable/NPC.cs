using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPC : InteractableObject
{
    public override void DoAction(GameObject player)
    {
        FlyerBag bag = player.transform.parent.GetComponent<FlyerBag>();

        List<Flyer> flyers = bag.GetFlyers();
        if (flyers.Count > 0)
        {
            Flyer flyer = flyers[Random.Range(0, flyers.Count)];
            bag.RemoveFlyer(flyer);
        }
        else
        {

        }
        //connect to info popup
    }

    void Start()
    {
        hoverName = "Give flyer";
    }

}
