using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Linq;

public class Dragger : MonoBehaviour
{
    private Camera _cam;
    private WireSpawner spawner;

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

    private void OnMouseUp()
    {
        Collider2D[] colliders = Physics2D.OverlapBoxAll(GetComponent<RectTransform>().position, GetComponent<BoxCollider2D>().size, 0);
        List<Collider2D> colliderList = colliders.Where(x => x.CompareTag("WireBackgroundPoint")).ToList();

        if (colliderList.Any())
        {
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
                Instantiate(spawner.explosionParticleEffect, collider.transform.position, spawner.explosionParticleEffect.transform.rotation);
            }
            else
            {
                spawner.OneFailed();
            }
        }
    }
}
