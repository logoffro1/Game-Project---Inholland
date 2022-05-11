using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Flyer 
{
    public string Title { get; set; }
    public string Body { get; set; }
    public float Points { get; set; }
    public int AmountToPrint { get; set; }

    public Flyer(string title, string body, float points, int amountToPrint)
    {
        Title = title;
        Body = body;
        Points = points;
        AmountToPrint = amountToPrint;
    }

}
