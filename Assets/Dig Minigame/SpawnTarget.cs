using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnTarget : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        float rnd = Random.Range(-0.5f, 0.5f);
        Debug.Log(rnd);
        transform.localPosition = new Vector3(rnd, -0.327f, 0);
    }

}
