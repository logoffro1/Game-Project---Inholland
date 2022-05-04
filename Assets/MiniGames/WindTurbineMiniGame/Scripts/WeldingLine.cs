using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeldingLine : MonoBehaviour
{

    LineRenderer lr;
    public bool isStarted;
    public LineRendererController lrController;
    WindTurbineMinigame minigame;
    private bool isObjMoving;
   public float speed = 0.1f;

    private Vector3 endPoint;
    void Start()
    {
        minigame = GetComponentInParent<WindTurbineMinigame>();
        speed = minigame.lineSpeed;
    }

    public void SetPosition(Vector3 pos)
    {
        gameObject.transform.position = pos;
        isStarted = true;
    }

    void Update()
    {
        if (Input.GetKey(KeyCode.Space)) {
            isObjMoving = true;
        }
        handleMovement(isObjMoving);      
    }

    private void handleMovement(bool isMoving)
    {
        if (isMoving)
        {
            if (isStarted)
            {
                transform.Translate(Vector3.right * speed * Time.deltaTime);
                if (Input.GetKey(KeyCode.W))
                {
                    transform.Translate(Vector3.up * 0.6f * Time.deltaTime);
                }
                else if (Input.GetKey(KeyCode.S))
                {

                    transform.Translate(Vector3.down * 0.6f * Time.deltaTime);
                }
            }
        }
    }

  
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (isStarted) 
        minigame.GameFinish(false);        
    }

}
