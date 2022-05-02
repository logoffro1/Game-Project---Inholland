using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(EdgeCollider2D))]
public class LineRendererController : MonoBehaviour
{
    // Start is called before the first frame update
    private LineRenderer lr;
    EdgeCollider2D ed2d;
    public Transform[] points { get; private set; }
   
    //Canvas Limits bottom -0.012 x , 0.307 y ,300.009 z,  
    //Canvas Limits top -0.004 x , -0.347 y ,300.009 z, 
   
    private void Start()
    {
        ed2d = this.GetComponent<EdgeCollider2D>();
        lr = GetComponent<LineRenderer>();
        lr.sortingOrder = 1;

        lr.material = new Material(Shader.Find("Sprites/Default"));
        lr.material.color = Color.red;
       
        setPoints();
        SetEdgeCollider(lr);
    }

    void SetEdgeCollider(LineRenderer lr)
    {
        List<Vector2> edges = new List<Vector2>();

        for(int i = 0; i < lr.positionCount; i++)
        {
            Vector3 lineRendererv3 = lr.GetPosition(i);
            edges.Add(new Vector2(lineRendererv3.x, lineRendererv3.y));
        }
        ed2d.SetPoints(edges);
    }
    private void setPoints()
    {
        points = AddRandomPoints(15);
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
        float maxLimit = 1.443f;
        float minLimit = -1.444f;
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
                obj1.transform.localPosition= new Vector3(minLimit, 0.300f, transform.parent.position.z);
                points[i] = obj1.transform;
            }
            GameObject obj = new GameObject();
            float xVal = lastXValue + incrementValue;
            obj.transform.localPosition = new Vector3(lastXValue + incrementValue, Random.Range(minY,maxY), transform.parent.position.z);
            points[i] = obj.transform;
            lastXValue = xVal;
        }
        return points;
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        Debug.Log("lostgame");

    }

}
