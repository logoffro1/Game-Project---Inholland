using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Dragger : MonoBehaviour
{
    /* Notes from Cosmin
     * 1. See Wire.cs & WireSpawner.cs
     * 2. If you'll use LineRenderer this whole class can mostly go, and there is no need for a canvas
     * 3. Try as much as possible to avoid using .Find() by name or by tag(extremely hard to manage the bigger the game gets),
     *    you can use .FindObjectOfType<Type> to get an Object of the specified type (You can use this to return an object (a class))
     *    For example: WireSpawner spawner = GameObject.FindObjectOfType<WireSpawner>();  will return the WireSpawner object that is in the scene
     *    Or you can use .FindObjectsOfType<Type> to get an array of objects, which can be used to get all the enemies, or all the wires, etc
     */
    private Camera _cam;
    GameObject spawner;

    //Game features
    private bool connected = false;
    private bool connectedCorrect = false;
    private Collider2D inCollisionWith;

    private void Start()
    {
        spawner = GameObject.Find("WireSpawner");
    }

    private void Awake()
    {
        _cam = GameObject.Find("RewireCamera").GetComponent<Camera>();
    }

    private void OnMouseDrag()
    {
        transform.position = GetMousePos();
    }

    Vector3 GetMousePos()
    {
        //Getting position of the mouse
        var mousePos = Input.mousePosition;
        var distance = Math.Abs(_cam.transform.position.z - transform.position.z);
        mousePos.z = distance;
        mousePos = _cam.ScreenToWorldPoint(mousePos);

        return mousePos;
    }

    private void OnMouseUp()
    {
        Debug.Log("OnMouseUp");

        if (inCollisionWith != null && inCollisionWith.tag == "WireBackgroundPoint")
        {
            //Putting state to connected
            connected = true;
            //Getting if they have the same parent, thus saying they are matching
            connectedCorrect = transform.parent.parent == inCollisionWith.transform.parent.parent;
            Debug.Log("Same parent?: " + connectedCorrect);

            //Locking in spot that they dropped on, for the endWire
            var collidedObject = inCollisionWith.transform.GetComponent<RectTransform>();
            var endPosition = collidedObject.transform.position;
            GetComponent<RectTransform>().position = endPosition;
        }

        if (connectedCorrect)
        {
            spawner.GetComponent<WireSpawner>().OneIsSuccessFul();
        }

        if (connected)
        {
            spawner.GetComponent<WireSpawner>().OneIsFinished();
        }

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log("OnTriggerEnter2D");
        inCollisionWith = collision;
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        Debug.Log("OnTriggerExit2D");
        inCollisionWith = null;
    }

}
