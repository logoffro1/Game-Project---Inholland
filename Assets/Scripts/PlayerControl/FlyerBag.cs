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
     
        //testing only
        AddFlyer(new Flyer("test", "test", 0.3f, 1));
        AddFlyer(new Flyer("test", "test", -1f, 1));
        AddFlyer(new Flyer("test", "test", 5f, 1));
    }


    public void AddFlyer(Flyer flyer)
    {
        if (!CanCollect()) return;

        flyers.Add(flyer);

        UIManager.Instance.SetFlyersText(flyers.Count, bagCapacity);
    }
    public List<Flyer> GetFlyers() => flyers;
    public void RemoveFlyer(Flyer flyer)
    {
        flyers.Remove(flyer);
        ProgressBar.Instance.ChangeSustainibility(flyer.Points, false);
        UIManager.Instance.SetFlyersText(flyers.Count, bagCapacity);
        Debug.Log("Flyers: " + flyers.Count);
    }

    public bool CanCollect() => flyers.Count < bagCapacity;
}
