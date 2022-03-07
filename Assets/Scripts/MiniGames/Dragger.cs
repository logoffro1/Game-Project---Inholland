using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Dragger : MonoBehaviour
{
    private Vector3 _dragOffset;
    private Camera _cam;
    GameObject spawner;

    private Vector3 startPoint;
    private CanvasGroup canvasGroup;

    public SpriteRenderer midWire;
    public RectTransform endPoint;

    //Game features
    public bool connected = false;
    public bool connectedCorrect = false;
    public Collider2D inCollisionWith;

    private void Start()
    {
        startPoint = transform.parent.position;
        canvasGroup = GetComponent<CanvasGroup>();
        spawner = GameObject.Find("WireSpawner");

    }

    private void Awake()
    {
        //Debug.Log("Awake");
        _cam = Camera.main;
    }

    private void OnMouseDown()
    {
        //Debug.Log("OnMouseDown");
        _dragOffset = transform.position - GetMousePos();
    }

    private void OnMouseDrag()
    {
        //Debug.Log("OnMouseDrag");

        var currentMousePos = GetMousePos();
        transform.position = currentMousePos + _dragOffset;

        midWire.size = new Vector2(Vector2.Distance(startPoint, currentMousePos) * 1.9f, midWire.size.y);
        midWire.transform.right = currentMousePos - startPoint;

    }

    Vector3 GetMousePos()
    {
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
            connected = true;
            connectedCorrect = transform.root == inCollisionWith.transform.root;
            Debug.Log("Same parent?: " + connectedCorrect);

            var collidedObject = inCollisionWith.transform.GetComponent<RectTransform>();
            GetComponent<RectTransform>().position = collidedObject.transform.position;

            Debug.Log(this.name + " DROPPED ON: " + inCollisionWith.name);
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
