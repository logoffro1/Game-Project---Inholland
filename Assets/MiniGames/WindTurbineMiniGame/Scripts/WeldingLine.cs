using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeldingLine : MonoBehaviour
{
 /*   Vector3 startPos;
    Vector3 endPos;
    Vector3 mousePos;
    Vector3 mouseDir;*/
    LineRenderer lr;
    public bool isStarted;
    public LineRendererController lrController;
    WindTurbineMinigame minigame;
    float speed = 0.1f;

    private Vector3 endPoint;
    void Start()
    {
        minigame = GetComponentInParent<WindTurbineMinigame>();
      /*  lrController = FindObjectOfType<LineRendererController>();
        endPoint = lrController.points[lrController.points.Length - 1].position;*/
    }

    public void SetPosition(Vector3 pos)
    {
        gameObject.transform.position = pos;
        isStarted = true;
    }

    // Update is called once per frame
    void Update()
    {
        if(isStarted)
            transform.Translate(Vector3.right * speed * Time.deltaTime);
        if (Input.GetKey(KeyCode.W))
        {
            transform.Translate(Vector3.up * 0.5f * Time.deltaTime);
        }
        else if (Input.GetKey(KeyCode.S))
        {

            transform.Translate(Vector3.down * 0.5f * Time.deltaTime);
        }
    }

  
    private void OnTriggerExit2D(Collider2D collision)
    {
        isStarted = false;
        minigame.GameFinish(false);
        Debug.Log("lostgametriggerWelding");
    }

}
