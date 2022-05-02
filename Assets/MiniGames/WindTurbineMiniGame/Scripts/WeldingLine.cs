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
    public LineRendererController lrController;
    float speed = 0.4f;
    void Start()
    {
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
            transform.Translate(Vector3.up * 0.8f * Time.deltaTime);
        }
        else if (Input.GetKey(KeyCode.S))
        {
            transform.Translate(Vector3.down * 0.8f * Time.deltaTime);
        }
    }
}
