using System.Collections;
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


    public void AddFlyer(Flyer flyer)
    {
        if (!CanCollect()) return;

        flyers.Add(flyer);

        UIManager.Instance.SetFlyersText(flyers.Count, bagCapacity);
    }
    public void RemoveFlyer(Flyer flyer)
    {
        flyers.Remove(flyer);
        ProgressBar.Instance.ChangeSustainibility(flyer.Points, false);
        UIManager.Instance.SetFlyersText(flyers.Count, bagCapacity);
    }

    public bool CanCollect() => flyers.Count < bagCapacity;
}
