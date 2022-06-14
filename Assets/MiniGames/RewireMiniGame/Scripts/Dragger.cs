using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Linq;

//Dragging logic of the rewire
public class Dragger : MonoBehaviour
{
    private Camera _cam;
    private WireSpawner spawner;
    private Vector3 originalPosition;

    private void Start()
    {
        spawner = FindObjectOfType<WireSpawner>();
        originalPosition = transform.position;
    }

    private void Awake()
    {
        //gets the camera
        _cam = transform.root.GetComponentInChildren<Camera>();
    }

    private void OnMouseDrag()
    {
        //changes position of the end wire
        if (Time.timeScale == 0) return;
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

    //When you release the mouse
    private void OnMouseUp()
    {
        Collider2D[] colliders = Physics2D.OverlapBoxAll(GetComponent<RectTransform>().position, GetComponent<BoxCollider2D>().size, 0);
        List<Collider2D> colliderList = colliders.Where(x => x.CompareTag("WireBackgroundPoint")).ToList();

        if (colliderList.Any())
        {
            //get the object, if the wire was correctly wired
            Collider2D collider = colliderList.Where(x => x.transform.parent == transform.parent).FirstOrDefault();

            //Locking in spot that they dropped on, for the endWire
            var collidedObject = collider != null ? collider.transform.GetComponent<RectTransform>() : colliderList.FirstOrDefault().transform.GetComponent<RectTransform>();
            GetComponent<RectTransform>().position = collidedObject.transform.position;

            //Changing states, and possibily ending the game
            spawner.OneIsFinished();
            gameObject.GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.FreezePositionX | RigidbodyConstraints2D.FreezePositionY;


            //Getting if they have the same parent, thus saying they are matching
            if (collider != null)
            {
                spawner.OneIsSuccessFul();
                spawner.InstantiateExplosion(collider.transform.position, transform.GetComponentInParent<Wire>().color);
            }
            else
            {
                spawner.OneFailed();
            }
        }
        else
        {
            //snaps back to original pos if placed on no colliders
            transform.position = originalPosition;
        }
    }
}
