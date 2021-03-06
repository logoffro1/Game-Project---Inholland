using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class LineRendererController : MonoBehaviour
{
    // This class is used for rendering different lines for wind turbine welding game.
    public LineRenderer lr;
   
    WeldingLine torch;
    public Transform[] points { get; private set; }
    public int difficultyLevel;

    WindTurbineMinigame minigame;
   
    
    private void Start()
    {
       
        lr = GetComponent<LineRenderer>();
        minigame = GetComponentInParent<WindTurbineMinigame>();
        lr.sortingOrder = 1;
        difficultyLevel = 10;
        lr.material = new Material(Shader.Find("Sprites/Default"));
        lr.material.color = Color.red;
       
        setPoints();    
        torch = FindObjectOfType<WeldingLine>();
        torch.SetPosition(points[0].position);

    }

    //Getting all the transforms in line renderer to create colliders.
     public Vector3[] GetPositions()
    {
        Vector3[] positions = new Vector3[lr.positionCount];
        lr.GetPositions(positions);
        return positions;
    }

    public float GetWidth() {

        return lr.startWidth;
    }

  
    //Setting random points to make a unique looking welding line according to game difficulty.
    private void setPoints()
    {
        points = AddRandomPoints(minigame.difficultyLevel);
        lr.positionCount = points.Length;
        for (int i = 0; i < points.Length; i++)
        {
            lr.SetPosition(i, points[i].position);
        }
    }


    //Adding random points based between two locations. Canvas limits are presented below so that line can be customizable without going out of those limits.
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
        float minY = -0.247f;
        float maxY = 0.207f;
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
}
