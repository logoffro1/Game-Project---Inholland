using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[RequireComponent(typeof(PolygonCollider2D))]
public class LineCollision : MonoBehaviour
{
    LineRendererController lc;
    List<Vector2> colliderPoints = new List<Vector2>();
    PolygonCollider2D poly2d;
    // Start is called before the first frame update
    void Awake()
    {
        lc = GetComponent<LineRendererController>();
        poly2d = GetComponent<PolygonCollider2D>();

    }

    private void LateUpdate()
    {
        Vector3[] positions = lc.GetPositions();
        if (positions.Length >= 2)
        {
            int numberOfLines = positions.Length - 1;
            poly2d.pathCount = numberOfLines;

            for (int i = 0; i < numberOfLines; i++)
            {
                List<Vector2> currentpos = new List<Vector2> {
                positions[i],
                positions[i+1]
                };
                List<Vector2> currentColliderPoints = CalculateColliderPoints(currentpos);
                poly2d.SetPath(i, currentColliderPoints.ConvertAll(p => (Vector2)transform.InverseTransformPoint(p)));
            }
        }
        else
        {
            poly2d.pathCount = 0;
        }
    }



        // Update is called once per frame
        void Update()
        {
            /*colliderPoints = CalculateColliderPoints();
            poly2d.SetPath(0, colliderPoints);*/
        }
        private List<Vector2> CalculateColliderPoints(List<Vector2> positions)
        {
           

            float width = lc.GetWidth();
            float m = (positions[1].y - positions[0].y) / (positions[1].x - positions[0].x);
            float deltaX = (width / 2f) * (m / Mathf.Pow(m * m + 1, 0.5f));
            float deltaY = (width / 2f) * (1 / Mathf.Pow(1 + m * m, 0.5f));
            Vector2[] offsets = new Vector2[2];
            offsets[0] = new Vector2(-deltaX, deltaY);
            offsets[1] = new Vector2(deltaX, -deltaY);
            List<Vector2> colliderpositions = new List<Vector2>
            {
             positions[0] + offsets[0],
             positions[1] + offsets[0],
             positions[1] + offsets[1],
             positions[0] +offsets[1]
             };
            return colliderpositions;
        }


    } 

