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
        _cam = Camera.main;
    }

    private void OnMouseDown()
    {
        _dragOffset = transform.position - GetMousePos();
    }

    private void OnMouseDrag()
    {
        var currentMousePos = GetMousePos();

        //Moving EndWire part
        transform.position = currentMousePos + _dragOffset;

        //Middle wire part
        midWire.size = new Vector2(Vector2.Distance(startPoint, currentMousePos) * 1.9f, midWire.size.y);
        midWire.transform.right = currentMousePos - startPoint;

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
            connectedCorrect = transform.root == inCollisionWith.transform.root;
            Debug.Log("Same parent?: " + connectedCorrect);

            //Locking in spot that they dropped on, for the endWire
            var collidedObject = inCollisionWith.transform.GetComponent<RectTransform>();
            var endPosition = collidedObject.transform.position;
            GetComponent<RectTransform>().position = endPosition;
            //Now for the midwire
            endPosition.x -= 3;
            midWire.size = new Vector2(Vector2.Distance(startPoint, endPosition) * 1.9f, midWire.size.y);
            midWire.transform.right = endPosition - startPoint;


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
