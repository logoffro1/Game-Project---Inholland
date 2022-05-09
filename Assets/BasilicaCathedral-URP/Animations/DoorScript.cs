using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorScript : MonoBehaviour
{
    public Animation anim;

    void Start()
    {
        anim = gameObject.GetComponent<Animation>();
    }

    void OnTriggerEnter(Collider col)
    {
        Debug.Log("Door Open!");
        anim.Play("Door_Open");
    }

    void OnTriggerExit(Collider col)
    {
        Debug.Log("Door Close!");
        anim.Play("Door_Close");
    }
}
