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
    WindTurbineMinigame minigame;
    float speed = 0.1f;
    void Start()
    {
        minigame = GetComponentInParent<WindTurbineMinigame>();
        isStarted = false;
      /*  lrController = FindObjectOfType<LineRendererController>();
        gameObject.transform.position = lrController.points[0].position;*/
        /*      lr = GetComponent<LineRenderer>();
              lr.material.color = Color.yellow;*/

        /*        Debug.Log($"real pos: {lrController.points[0].position}");*/
        /*  startPos = lrController.points[0].position;
          gameObject.transform.position = startPos;*/

    }

    public void SetPosition(Vector3 pos)
    {
        gameObject.transform.position = pos;
    }

    // Update is called once per frame
    void Update()
    {
        
        if(isStarted)
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
        isStarted = false;
        minigame.GameFinish(false);
        Debug.Log("lostgametriggerWelding");
    }
   
    private void OnCollisionExit2D(Collision2D collision)
    {
        isStarted = false;
        Debug.Log("lostgameCollisionwelding");
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
            Debug.Log("Touchedcollisionwelding");
        
    }
    bool GameEnded()
    {
        if (gameObject.transform.localPosition == lrController.points[lrController.points.Length-1].localPosition)
            return true;
        else
            return false;
    }
}
