using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LineRendererController : MonoBehaviour
{
    // Start is called before the first frame update
    private LineRenderer lr;
    private Transform[] points;
    //Canvas Limits bottom -0.012 x , 0.307 y ,300.009 z,  
    //Canvas Limits top -0.004 x , -0.347 y ,300.009 z, 
   
    private void Start()
    {
        lr = GetComponent<LineRenderer>();
        lr.sortingOrder = 1;
        lr.material = new Material(Shader.Find("Sprites/Default"));
        lr.material.color = Color.red;
        setPoints();
    }
    private void setPoints()
    {
        points = AddRandomPoints(20);
        lr.positionCount = points.Length;
        for (int i = 0; i < points.Length; i++)
        {
            lr.SetPosition(i, points[i].position);
        }
    }

    private Transform[] AddRandomPoints(int difficultyLevel)
    {
        //Canvas Limits bottom -0.012 x , 0.307 y ,300.009 z  
        //Canvas Limits top -0.004 x , -0.347 y ,300.009 z
        //Canvas Limits right side 1.443 x , -0.032 y ,300.009 z 
        //Canvas Limits left side -1.444 x , -0.057 y ,300.009 z
        Transform[] points = new Transform[difficultyLevel];
        float maxLimit = 2.2f;
        float minLimit = -2.2f;
        float completeLimit = maxLimit - minLimit;
        float incrementValue = completeLimit / points.Length;
        float minY = -0.347f;
        float maxY = 0.307f;
        float lastXValue = minLimit;

        for (int i = 0; i < points.Length; i++)
        {
            if (i == 0)
            {
                GameObject obj1 = new GameObject();
                obj1.transform.localPosition= new Vector3(minLimit, 0.300f, transform.parent.position.z + 1);
                points[i] = obj1.transform;
            }
            GameObject obj = new GameObject();
            float xVal = lastXValue + incrementValue;
            obj.transform.localPosition = new Vector3(lastXValue + incrementValue, Random.Range(minY,maxY), transform.parent.position.z+1);
            points[i] = obj.transform;
            lastXValue = xVal;
        }
        return points;
    }
}
