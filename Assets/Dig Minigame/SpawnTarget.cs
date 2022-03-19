using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnTarget : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        float rnd = Random.Range(-0.45f,0.45f);
        Debug.Log(rnd);
        transform.position = new Vector3(69,69,69);
    }

    // Update is called once per frame
    void Update()
    {
       
    }
}
