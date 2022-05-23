using System.Collections.Generic;
using UnityEngine;

public class NPC : InteractableObject // human NPC
{
    private bool receivedFlyer = false;
    public override void DoAction(GameObject player) // interact with NPC
    {
        if (receivedFlyer) return; // limit to 1 flyer per NPC
        FlyerBag bag = player.transform.parent.GetComponent<FlyerBag>();

        List<Flyer> flyers = bag.GetFlyers();
        if (flyers.Count > 0)
        {
            // give the NPC a random flyer from the bag
            Flyer flyer = flyers[Random.Range(0, flyers.Count)];
            bag.RemoveFlyer(flyer);
            receivedFlyer = true;

            // increase achievement
            FindObjectOfType<GlobalAchievements>().GetAchievement("activist").CurrentCount++;
        }
    }

    void Start()
    {
        hoverName = "Give flyer";
    }

}
