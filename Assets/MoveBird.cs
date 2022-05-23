using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveBird : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        this.transform.Translate(Vector3.forward * Time.deltaTime * 1.5f); 
    }
}
