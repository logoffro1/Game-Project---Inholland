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
    void Start()
    {
        lr = GetComponent<LineRenderer>();
        startPos = new Vector3(-1.5f, 0.300f, transform.parent.position.z -4);
        gameObject.transform.position = startPos;

    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
