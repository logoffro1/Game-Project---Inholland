using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnTarget : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        //picks a random number between the left and right limits of the
        //dig at the right moment minigame and creates the target spot to dig in that randomly selected area
        float rnd = Random.Range(-0.5f, 0.5f);
        Debug.Log(rnd);
        transform.localPosition = new Vector3(rnd, -0.327f, 0);
    }

}
