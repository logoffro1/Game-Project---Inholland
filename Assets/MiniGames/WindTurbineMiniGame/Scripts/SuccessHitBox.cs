using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SuccessHitBox : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject lineRenderer;
    WindTurbineMinigame minigame;
    LineRendererController lrC;
    void Start()
    {
        minigame = GetComponentInParent<WindTurbineMinigame>();
        Physics2D.IgnoreCollision(lineRenderer.GetComponent<PolygonCollider2D>(), GetComponent<Collider2D>());
        lrC = lineRenderer.GetComponent<LineRendererController>();
        gameObject.transform.position = lrC.points[lrC.points.Length - 1].position;
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        minigame.GameFinish(true);
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
