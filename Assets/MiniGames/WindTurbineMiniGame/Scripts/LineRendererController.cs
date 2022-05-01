using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LineRendererController : MonoBehaviour
{
    // Start is called before the first frame update
    private LineRenderer lr;
    public Transform[] points;

    private void Start()
    {
        lr = GetComponent<LineRenderer>();
        lr.positionCount = points.Length;
        this.points = points;
    }

    private void Update()
    {
        for(int i=0;i<points.Length;i++)
        {
            lr.SetPosition(i, points[i].position);
        }
    }

}
