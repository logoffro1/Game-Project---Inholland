using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SuccessHitBox : MonoBehaviour
{
    //This class is used for win condition of welding mini game by checking triggers between torch and welding line collider based on the line collision class initialization we made.
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
