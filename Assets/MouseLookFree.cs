using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseLookFree : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        //not sure if this should be none or Locked
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        GameObject.FindObjectOfType<MouseLook>().canRotate = true;
        GameObject.FindObjectOfType<PlayerMovement>().canMove = true;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
