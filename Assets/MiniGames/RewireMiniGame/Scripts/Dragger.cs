using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Dragger : MonoBehaviour
{
    private Camera _cam;
    private WireSpawner spawner;

    //Game features
    private bool connectedCorrect = false;
    private Collider2D inCollisionWith;

    private void Start()
    {
        spawner = FindObjectOfType<WireSpawner>();
    }

    private void Awake()
    {
        _cam = transform.root.GetComponentInChildren<Camera>();
    }

    private void OnMouseDrag()
    {
        transform.position = GetMousePos();
    }

    private Vector3 GetMousePos()
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
        if (inCollisionWith != null && inCollisionWith.CompareTag("WireBackgroundPoint"))
        {
            //Getting if they have the same parent, thus saying they are matching
            connectedCorrect = transform.parent.parent == inCollisionWith.transform.parent;

            //Locking in spot that they dropped on, for the endWire
            var collidedObject = inCollisionWith.transform.GetComponent<RectTransform>();
            var endPosition = collidedObject.transform.position;
            GetComponent<RectTransform>().position = endPosition;

            //Changing states, and possibily ending the game
            spawner.OneIsFinished();
        }

        if (connectedCorrect)
        {
            spawner.OneIsSuccessFul();
        }

        if (!connectedCorrect && inCollisionWith != null)
        {
            spawner.OneFailed();
        }

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        inCollisionWith = collision;
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        inCollisionWith = null;
    }

}
