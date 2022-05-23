using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Line logic between startof wire and end of wire
public class LineController : MonoBehaviour
{
    private LineRenderer lineRenderer;
    private Transform[] points;
  
    private void Awake()
    {
        lineRenderer = GetComponent<LineRenderer>();
        SpriteRenderer spriteRenderer = GetComponent<SpriteRenderer>();

        lineRenderer.sortingLayerID = spriteRenderer.sortingLayerID;
        lineRenderer.sortingOrder = spriteRenderer.sortingOrder;
    }

    //Gets starting point of line
    public void SetUpLine(Transform[] points)
    {
        lineRenderer.positionCount = points.Length;
        this.points = points;
    }

    //Stretches the line
    private void Update()
    {
        for (int i = 0; i < points.Length; i++)
        {
            lineRenderer.SetPosition(i, points[i].position);
        }
    }

}
