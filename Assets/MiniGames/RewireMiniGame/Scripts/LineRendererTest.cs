using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LineRendererTest : MonoBehaviour
{
    [SerializeField] private Transform[] points;
    [SerializeField] private LineController line;

    //Sets up the line
    private void Start()
    {
        line.SetUpLine(points);
    }
}
