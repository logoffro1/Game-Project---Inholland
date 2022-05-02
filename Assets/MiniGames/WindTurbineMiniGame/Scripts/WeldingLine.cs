using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeldingLine : MonoBehaviour
{
    Vector3 startPos;
    Vector3 endPos;
    Vector3 mousePos;
    Vector3 mouseDir;
    LineRenderer lr;
    private bool isStarted;
    public LineRendererController lrController;
    float speed = 0.1f;
    void Start()
    {
        isStarted = false;
      /*  gameObject.transform.position = new Vector2(lrController.points[0].localPosition.x, lrController.points[0].localPosition.y);*/
        /*      lr = GetComponent<LineRenderer>();
              lr.material.color = Color.yellow;*/

        /*        Debug.Log($"real pos: {lrController.points[0].position}");*/
        /*  startPos = lrController.points[0].position;
          gameObject.transform.position = startPos;*/

    }

    // Update is called once per frame
    void Update()
    {
      
        transform.Translate(Vector3.right * speed * Time.deltaTime);

        if (Input.GetKey(KeyCode.W))
        {
            isStarted = true;
            transform.Translate(Vector3.up * 0.5f * Time.deltaTime);
        }
        else if (Input.GetKey(KeyCode.S))
        {

            isStarted = true;
            transform.Translate(Vector3.down * 0.5f * Time.deltaTime);
        }
/*
        if (GameEnded())
        {
            isStarted = false;
        }*/
    }

  
    private void OnTriggerExit2D(Collider2D collision)
    {
        Debug.Log("lostgame");
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
            Debug.Log("Touched");
        
    }
    bool GameEnded()
    {
        Debug.Log($"P1{gameObject.transform.localPosition} : P2 {lrController.points[lrController.points.Length - 1].transform.localPosition}");
        if (gameObject.transform.localPosition == lrController.points[lrController.points.Length-1].localPosition)
            return true;
        else
            return false;
    }
}
