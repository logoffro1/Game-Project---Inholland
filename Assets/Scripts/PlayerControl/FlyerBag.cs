using System.Collections.Generic;
using UnityEngine;

public class FlyerBag : MonoBehaviour
{
    private List<Flyer> flyers;
    private int bagCapacity = 15;

    void Start()
    {
        flyers = new List<Flyer>();
    }
    public void AddFlyer(Flyer flyer) // if can collect, add new flyer to bag
    {
        if (!CanCollect()) return;

        flyers.Add(flyer);

        UIManager.Instance.SetFlyersText(flyers.Count, bagCapacity);
    }
    public List<Flyer> GetFlyers() => flyers;
    public void RemoveFlyer(Flyer flyer) // Give flyer to NPC
    {

        //remove flyer from bag and change sustainability
        flyers.Remove(flyer);
        ProgressBar.Instance.ChangeSustainibility(flyer.Points, false);
        UIManager.Instance.SetFlyersText(flyers.Count, bagCapacity);
    }

    public bool CanCollect() => flyers.Count < bagCapacity;
}
